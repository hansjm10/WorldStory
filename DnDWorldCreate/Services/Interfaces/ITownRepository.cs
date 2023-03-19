using DnDWorldCreate.Data;

namespace DnDWorldCreate.Services.Interfaces
{
    public interface ITownRepository : IRepository<Town>
    {
        Task<IEnumerable<Town>> GetTownsByRegionIdAsync(int regionId);
    }
}
