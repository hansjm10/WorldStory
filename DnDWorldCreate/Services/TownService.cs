using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DnDWorldCreate.Services
{
    public class TownService
    {
        private readonly ITownRepository _townRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public TownService(ITownRepository townRepository, IDbContextFactory<DnDWorldContext> dbContextFactory)
        {
            _townRepository = townRepository;
            _contextFactory = dbContextFactory;
        }
        public async Task<Town> AddTownAsync(Town town)
        {

            if (town == null)
            {
                throw new ArgumentNullException(nameof(town));
            }

            using var context = _contextFactory.CreateDbContext();
            await _townRepository.AddAsync(town, context);
            await _townRepository.SaveChangesAsync(context);

            return town;

        }
        public async Task<Town> GetTownByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var town = await _townRepository.GetByIdAsync(id, context) ?? throw new ArgumentException($"Cannot find town: {id}");
            return town;

        }
        public async Task<IEnumerable<Town>> GetAllTownsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await _townRepository.GetAllAsync(context);

        }
        public async Task<IReadOnlyList<Town>> GetAllTownsReadOnlyAsync()
        {
            var towns = (await GetAllTownsAsync()).ToList();
            return towns;

        }
        public async Task UpdateTownAsync(Town town)
        {

            if (town == null)
            {
                throw new ArgumentNullException(nameof(town));
            }
            using var context = _contextFactory.CreateDbContext();
            _townRepository.Update(town, context);
            await _townRepository.SaveChangesAsync(context);

        }
        public async Task DeleteTownAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var town = await _townRepository.GetByIdAsync(id,context);

            if (town == null)
            {
                throw new InvalidOperationException($"Town with ID {id} not found.");
            }

            _townRepository.Remove(town, context);
            await _townRepository.SaveChangesAsync(context);

        }
        public async Task<int> SaveChangesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await _townRepository.SaveChangesAsync(context);

        }
        public async Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await _townRepository.GetTownsByRegionIdAsync(regionId, context);

        }

    }
}
