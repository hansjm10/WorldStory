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
            _regions = new List<Region>();
        }
        public async Task<IEnumerable<Region>> GetRegionsAsync(bool forceReload = false)
        {
            
            if (_regions == null || forceReload)
            {
                _regions = await _regionService.GetAllRegionsAsync();
            }

            return _regions;
        }
        public async Task<IReadOnlyList<Region>> GetRegionsReadOnlyAsync(bool forceReload = false)
        {
            var regions = await _regionService.GetAllRegionsAsync();
            return (IReadOnlyList<Region>)regions;
        }
        public async Task<IEnumerable<Region>> GetEditableRegionsAsync(bool forceReload = false)
        {
            if (_regions == null || forceReload)
            {
                var regions = (await _regionService.GetAllRegionsAsync()).ToList();
                regions.Remove(regions.First(x => x.Id == 0));
                _regions = regions;
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
        public async Task<Region> GetUnassignedRegion()
        {
            var unAssignedRegion = await _regionService.GetRegionByIdAsync(0);
            return unAssignedRegion;
        }
    }
}
