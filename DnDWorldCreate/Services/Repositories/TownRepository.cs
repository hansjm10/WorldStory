using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services.Repositories
{
    public class TownRepository : EfRepository<Town>, ITownRepository
    {
        public TownRepository(DnDWorldContext context) : base(context) { }
        public async Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId)
        {
            return await _context.Towns.Where(t => t.RegionId == regionId).ToListAsync();
        }
    }
}
