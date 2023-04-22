using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DnDWorldCreate.Services
{
    public class TownService : BaseService<Town>
    {
        private readonly ITownRepository _townRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public TownService(ITownRepository townRepository, IDbContextFactory<DnDWorldContext> dbContextFactory) : base (townRepository, dbContextFactory)
        {
            _townRepository = townRepository;
            _contextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await _townRepository.GetTownsByRegionIdAsync(regionId, context);

        }

    }
}
