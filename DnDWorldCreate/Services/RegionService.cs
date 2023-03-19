using DnDWorldCreate.Data;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class RegionService
    {
        private readonly IRepository<Region> _regionRepository;
        public RegionService(IRepository<Region> regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<Region> AddRegionAsync(Region region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            await _regionRepository.AddAsync(region);
            await _regionRepository.SaveChangesAsync();

            return region;
        }
        public async Task<Region> GetRegionByIdAsync(int id)
        {
            return await _regionRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            return await _regionRepository.GetAllAsync();
        }
        public async Task UpdateRegionAsync(Region updatedRegion)
        {
            if (updatedRegion == null)
            {
                throw new ArgumentNullException(nameof(updatedRegion));
            }

            var originalRegion = await _regionRepository.GetByIdAsync(updatedRegion.Id);
            if (originalRegion == null)
            {
                throw new ArgumentException($"Region with ID {updatedRegion.Id} not found.");
            }

            originalRegion.Name = updatedRegion.Name;
            originalRegion.Description = updatedRegion.Description;

            _regionRepository.Update(originalRegion);
            await _regionRepository.SaveChangesAsync();
        }
        public async Task DeleteRegionAsync(int id)
        {
            var region = await _regionRepository.GetByIdAsync(id);

            if (region == null)
            {
                throw new InvalidOperationException($"Region with ID {id} not found.");
            }

            _regionRepository.Remove(region);
            await _regionRepository.SaveChangesAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _regionRepository.SaveChangesAsync();
        }
    }
}
