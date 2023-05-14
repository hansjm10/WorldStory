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
        /// <summary>
        /// Updates the BaseStats object in the database.
        /// </summary>
        /// <param name="baseStats">The BaseStats object to be updated.</param>
        /// <param name="saveChanges">Flag to indicate if changes should be saved.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
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

            _baseStatsRepository.Update(baseStats, context);

            if (saveChanges)
            {
                await _baseStatsRepository.SaveChangesAsync(context);
            }
        }
    }
}