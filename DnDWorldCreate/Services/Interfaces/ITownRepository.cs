using DnDWorldCreate.Data;
using DnDWorldCreate.Data.Entitys;

namespace DnDWorldCreate.Services.Interfaces
{
    public interface ITownRepository : IRepository<Town>
    {
        Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId, DnDWorldContext context);
    }
}
