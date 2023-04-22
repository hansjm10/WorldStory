using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data;
using Microsoft.EntityFrameworkCore;

namespace DnDWorldCreate.Services
{
    public class BuildingService : BaseService<Buildings>
    {
        private readonly IRepository<Buildings> _buildingRepository;
        private readonly IDbContextFactory<DnDWorldContext> _contextFactory;
        public BuildingService(IRepository<Buildings> buildingRepository, IDbContextFactory<DnDWorldContext> contextFactory) : base (buildingRepository, contextFactory)
        {
            _buildingRepository = buildingRepository;
            _contextFactory = contextFactory;
        }
    }
}