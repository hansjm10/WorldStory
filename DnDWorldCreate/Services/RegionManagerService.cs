using DnDWorldCreate.Data;

namespace DnDWorldCreate.Services
{
    public class RegionManagerService
    {
        private readonly RegionService _regionService;
        private readonly TownService _townService;
        private IEnumerable<Region> _regions;

        public RegionManagerService(RegionService regionService, TownService townService)
        {
            _regionService = regionService;
            _townService = townService;
        }
        public async Task<IEnumerable<Region>> GetRegionsAsync(bool forceReload = false)
        {
            
            if (_regions == null || forceReload)
            {
                _regions = await _regionService.GetAllRegionsAsync();
            }

            return _regions;
        }
        public async Task DeleteRegionWithTownsAsync(int regionId)
        {
            var towns = await _townService.GetTownsByRegionIdAsync(regionId);

            foreach (var town in towns)
            {
                await _townService.DeleteTownAsync(town.Id);
            }
            await _regionService.DeleteRegionAsync(regionId);
        }
    }
}
