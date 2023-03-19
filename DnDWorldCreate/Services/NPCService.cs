using DnDWorldCreate.Data;
using DnDWorldCreate.Services.Interfaces;

namespace DnDWorldCreate.Services
{
    public class NPCService
    {
        private readonly IRepository<NPC> _nonPlayerCharacterRepository;
        public NPCService(IRepository<NPC> nonPlayerCharacterRepository)
        {
            _nonPlayerCharacterRepository = nonPlayerCharacterRepository;
        }
        public async Task<NPC> AddNPCAsync(NPC nonPlayerCharacter)
        {
            if (nonPlayerCharacter == null)
            {
                throw new ArgumentNullException(nameof(nonPlayerCharacter));
            }

            await _nonPlayerCharacterRepository.AddAsync(nonPlayerCharacter);
            await _nonPlayerCharacterRepository.SaveChangesAsync();

            return nonPlayerCharacter;
        }
        public async Task<NPC> GetNPCByIdAsync(int id)
        {
            return await _nonPlayerCharacterRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<NPC>> GetAllNPCsAsync()
        {
            return await _nonPlayerCharacterRepository.GetAllAsync();
        }
        public async Task UpdateNPCAsync(NPC nonPlayerCharacter)
        {
            if (nonPlayerCharacter == null)
            {
                throw new ArgumentNullException(nameof(nonPlayerCharacter));
            }

            _nonPlayerCharacterRepository.Update(nonPlayerCharacter);
            await _nonPlayerCharacterRepository.SaveChangesAsync();
        }
        public async Task DeleteNPCAsync(int id)
        {
            var nonPlayerCharacter = await _nonPlayerCharacterRepository.GetByIdAsync(id);

            if (nonPlayerCharacter == null)
            {
                throw new InvalidOperationException($"NPC with ID {id} not found.");
            }

            _nonPlayerCharacterRepository.Remove(nonPlayerCharacter);
            await _nonPlayerCharacterRepository.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _nonPlayerCharacterRepository.SaveChangesAsync();
        }
    }
}
