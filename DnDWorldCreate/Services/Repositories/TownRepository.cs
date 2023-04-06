using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DnDWorldCreate.Services.Repositories
{
    public class TownRepository : EfRepository<Town>, ITownRepository
    {
        public TownRepository(IDbContextFactory<DnDWorldContext> context) : base(context) { }

        public async Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId, DnDWorldContext context)
        {
            return await context.Towns.Where(t => t.RegionId == regionId).ToListAsync();
        }
    }
}
