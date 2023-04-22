using DnDWorldCreate.Data.Entitys;
using System.Reflection.Metadata.Ecma335;

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
                _regions = await _regionService.GetAllAsync();
            }

            return _regions;

        }
        public async Task<IReadOnlyList<Region>> GetRegionsReadOnlyAsync(bool forceReload = false)
        {

            var regions = await _regionService.GetAllAsync();
            return (IReadOnlyList<Region>)regions;

        }
        public async Task<IEnumerable<Region>> GetEditableRegionsAsync(bool forceReload = false)
        {

            if (_regions == null || forceReload)
            {
                var regions = (await _regionService.GetAllAsync()).ToList();
                regions.Remove(regions.First(x => x.Id == 1));
                _regions = regions;
            }
            return _regions;

        }
        public async Task DeleteRegionWithTownsAsync(int regionId)
        {
            var towns = await _townService.GetTownsByRegionIdAsync(regionId);

            foreach (var town in towns)
            {
                await _townService.DeleteAsync(town.Id);
            }
            await _regionService.DeleteAsync(regionId);

        }
        public async Task<Region> GetUnassignedRegion()
        {

            var unAssignedRegion = await _regionService.GetByIdAsync(1);
            return unAssignedRegion;

        }
    }
}
