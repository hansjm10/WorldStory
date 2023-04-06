using DnDWorldCreate.Data.Entitys;

namespace DnDWorldCreate.Services.Interfaces
{
    public interface INPCServiceBase
    {
        Task<NPC> AddNPCAsync(NPC npc);
        Task<NPC> GetNPCByIdAsync(int id);
        Task<IEnumerable<NPC>> GetAllNPCsAsync();
        Task<IReadOnlyList<NPC>> GetAllNPCsReadOnlyAsync();
        Task DeleteNPCAsync(int id);
        Task<int> SaveChangesAsync();
    }
}
