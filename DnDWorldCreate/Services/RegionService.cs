using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class RegionService
    {
        private readonly IRepository<Region> _regionRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;

        public RegionService(IRepository<Region> regionRepository, IDbContextFactory<DnDWorldContext> contextFactory)
        {
            _regionRepository = regionRepository;
            _contextFactory = contextFactory;
        }

        public async Task<Region> AddRegionAsync(Region? region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            using var context = _contextFactory.CreateDbContext();
            await _regionRepository.AddAsync(region, context);
            await _regionRepository.SaveChangesAsync(context);

            return region;
        }
        public async Task<Region> GetRegionByIdAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var region = await _regionRepository.GetByIdAsync(id, context) ?? throw new ArgumentException($"Cannot find region: {id}");
            return region;
        }
        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await _regionRepository.GetAllAsync(context);
        }
        public async Task UpdateRegionAsync(Region updatedRegion)
        {
            if (updatedRegion == null)
            {
                throw new ArgumentNullException(nameof(updatedRegion));
            }

            using var context = _contextFactory.CreateDbContext();
            var originalRegion = await _regionRepository.GetByIdAsync(updatedRegion.Id, context);
            if (originalRegion == null)
            {
                throw new ArgumentException($"Region with ID {updatedRegion.Id} not found.");
            }

            originalRegion.Name = updatedRegion.Name;
            originalRegion.Description = updatedRegion.Description;

            _regionRepository.Update(originalRegion, context);
            await _regionRepository.SaveChangesAsync(context);
        }
        public async Task DeleteRegionAsync(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var region = await _regionRepository.GetByIdAsync(id, context);

            if (region == null)
            {
                throw new InvalidOperationException($"Region with ID {id} not found.");
            }
            if (region.Id == 0)
            {
                throw new InvalidOperationException("The Unassigned region cannot be deleted");
            }

            _regionRepository.Remove(region, context);
            await _regionRepository.SaveChangesAsync(context);
        }
        public async Task<int> SaveChangesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await _regionRepository.SaveChangesAsync(context);
        }
    }
}
