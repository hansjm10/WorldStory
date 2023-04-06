using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data.Stats;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class BaseStatsService
    {
        private readonly IRepository<BaseStats> _baseStatsRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;

        public BaseStatsService(IRepository<BaseStats> baseStatsRepository, IDbContextFactory<DnDWorldContext> contextFactory)
        {
            _baseStatsRepository = baseStatsRepository;
            _contextFactory = contextFactory;
        }
        public async Task<BaseStats> AddBaseStatsAsync(BaseStats baseStatsRepository)
        {
            if (baseStatsRepository == null)
            {
                throw new ArgumentNullException(nameof(baseStatsRepository));
            }
            using var context = _contextFactory.CreateDbContext();

            await _baseStatsRepository.AddAsync(baseStatsRepository, context);
            await _baseStatsRepository.SaveChangesAsync(context);

            return baseStatsRepository;
        }
        public async Task<BaseStats> GetBaseStatsByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var baseStats = await _baseStatsRepository.GetByIdAsync(id, context) ?? throw new ArgumentException($"Cannot find Stats: {id}");
            return baseStats;
        }
        public async Task<IEnumerable<BaseStats>> GetAllBaseStatsAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _baseStatsRepository.GetAllAsync(context);
        }
        public async Task<IReadOnlyList<BaseStats>> GetAllBaseStatsReadOnlyAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            var baseStats = await _baseStatsRepository.GetAllAsync(context);
            return baseStats.ToList();
        }
        public async Task UpdateBaseStatsAsync(BaseStats baseStats, bool saveChanges = true)
        {
            if (baseStats == null)
            {
                throw new ArgumentNullException(nameof(baseStats));
            }

            using var context = _contextFactory.CreateDbContext();

            var originalBaseStats = await _baseStatsRepository.GetByIdAsync(baseStats.Id, context);
            if (originalBaseStats == null)
            {
                throw new ArgumentNullException(nameof(originalBaseStats));
            }

            originalBaseStats.Strength = baseStats.Strength;
            originalBaseStats.Constitution = baseStats.Constitution;
            originalBaseStats.Dexterity = baseStats.Dexterity;
            originalBaseStats.Wisdom = baseStats.Wisdom;
            originalBaseStats.Intelligence = baseStats.Intelligence;
            originalBaseStats.Charisma = baseStats.Charisma;

            _baseStatsRepository.Update(originalBaseStats, context);

            if (saveChanges)
            {
                await _baseStatsRepository.SaveChangesAsync(context);
            }
        }
        public async Task DeleteBaseStatsAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();

            var baseStats = await _baseStatsRepository.GetByIdAsync(id,context);

            if (baseStats == null)
            {
                throw new InvalidOperationException($"Stats with ID {id} not found.");
            }

            _baseStatsRepository.Remove(baseStats, context);
            await _baseStatsRepository.SaveChangesAsync(context);
        }
        public async Task<int> SaveChangesAsync()
        {
            using var context = _contextFactory.CreateDbContext();

            return await _baseStatsRepository.SaveChangesAsync(context);
        }
    }
}