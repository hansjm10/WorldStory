using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data.Stats;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class BaseStatsService : BaseService<BaseStats>
    {
        private readonly IRepository<BaseStats> _baseStatsRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;

        public BaseStatsService(IRepository<BaseStats> baseStatsRepository, IDbContextFactory<DnDWorldContext> contextFactory) : base(baseStatsRepository, contextFactory)
        {
            _baseStatsRepository = baseStatsRepository;
            _contextFactory = contextFactory;
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
    }
}