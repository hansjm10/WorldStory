using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class NPCService : INPCServiceBase
    {
        private readonly IRepository<NPC> _nonPlayerCharacterRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public NPCService(IRepository<NPC> nonPlayerCharacterRepository, IDbContextFactory<DnDWorldContext> contextFactory)
        {
            _nonPlayerCharacterRepository = nonPlayerCharacterRepository;
            _contextFactory = contextFactory;
        }
        public async Task<NPC> AddNPCAsync(NPC nonPlayerCharacter)
        {
            if (nonPlayerCharacter == null)
            {
                throw new ArgumentNullException(nameof(nonPlayerCharacter));
            }
            using var context = _contextFactory.CreateDbContext();

            await _nonPlayerCharacterRepository.AddAsync(nonPlayerCharacter, context);
            await _nonPlayerCharacterRepository.SaveChangesAsync(context);

            return nonPlayerCharacter;

        }
        public async Task<NPC> GetNPCByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            return await _nonPlayerCharacterRepository.GetByIdAsync(id, context) ?? throw new ArgumentException($"Cannot find NPC: {id}");
        }
        public async Task<IEnumerable<NPC>> GetAllNPCsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _nonPlayerCharacterRepository.GetAllAsync(context);
        }
        public async Task<IReadOnlyList<NPC>> GetAllNPCsReadOnlyAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var npcs = await _nonPlayerCharacterRepository.GetAllAsync(context);
            return npcs.ToList();

        }
        public async Task UpdateNPCAsync(NPC updatedNpc)
        {

            if (updatedNpc == null)
            {
                throw new ArgumentNullException(nameof(updatedNpc));
            }

            using var context = _contextFactory.CreateDbContext();

            // Get the original NPC from the repository
            var originalNpc = await _nonPlayerCharacterRepository.GetByIdAsync(updatedNpc.Id, context);
            if (originalNpc == null)
            {
                throw new ArgumentException($"NPC with ID {updatedNpc.Id} not found.");
            }

            // Update NPC properties
            originalNpc.Name = updatedNpc.Name;
            originalNpc.Age = updatedNpc.Age;
            originalNpc.Occupation = updatedNpc.Occupation;
            originalNpc.Backstory = updatedNpc.Backstory;
            originalNpc.TownId = updatedNpc.TownId;
            originalNpc.ImagePath = updatedNpc.ImagePath;
            originalNpc.Alignment = updatedNpc.Alignment;

            //Sanitize personality traits to store it as a string in the database.
            originalNpc.PersonalityTraits = updatedNpc.PersonalityTraits.Select(trait => trait.Replace(";", string.Empty)).ToList();

            await _nonPlayerCharacterRepository.UpdateAsync(originalNpc, context);
        }
        public async Task DeleteNPCAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var nonPlayerCharacter = await _nonPlayerCharacterRepository.GetByIdAsync(id,context);

            if (nonPlayerCharacter == null)
            {
                throw new InvalidOperationException($"NPC with ID {id} not found.");
            }
            
            _nonPlayerCharacterRepository.Remove(nonPlayerCharacter, context);
            await _nonPlayerCharacterRepository.SaveChangesAsync(context);

        }
        public async Task<int> SaveChangesAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _nonPlayerCharacterRepository.SaveChangesAsync(context);
        }
        public async Task<IEnumerable<NPC>> GetAllNPCsIncluding()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _nonPlayerCharacterRepository.GetAllIncludingAsync(context, npc=> npc.Include(n => n.Stats));
        }
    }
}
