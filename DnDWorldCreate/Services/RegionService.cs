using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class RegionService : BaseService<Region>
    {
        private readonly IRepository<Region> _regionRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;

        public RegionService(IRepository<Region> regionRepository, IDbContextFactory<DnDWorldContext> contextFactory) : base(regionRepository, contextFactory)
        {
            _regionRepository = regionRepository;
            _contextFactory = contextFactory;
        }

    }
}
