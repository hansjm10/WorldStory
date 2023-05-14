using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class BuildingService : BaseService<Building>
    {
        private readonly IRepository<Building> _buildingRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public BuildingService(IRepository<Building> buildingRepository, IDbContextFactory<DnDWorldContext> contextFactory) : base (buildingRepository, contextFactory)
        {
            _buildingRepository = buildingRepository;
            _contextFactory = contextFactory;
        }
    }
}