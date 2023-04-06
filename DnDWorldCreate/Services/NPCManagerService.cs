using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Services.Interfaces;

namespace DnDWorldCreate.Services
{
    public class NPCManagerService : INPCServiceBase
    {
        private readonly NPCService _npcService;
        private readonly BaseStatsService _baseStatsService;
        public NPCManagerService(NPCService npcService, BaseStatsService baseStatsService)
        {
            _npcService = npcService;
            _baseStatsService = baseStatsService;
        }
        public async Task UpdateNpc(NPC npc)
        {
            if (npc == null)
            {
                return;
            }

            await _npcService.UpdateNPCAsync(npc);

            if (npc.Stats != null)
            {
                await _baseStatsService.UpdateBaseStatsAsync(npc.Stats, true);
            }
            else
            {
                await _npcService.SaveChangesAsync();
            }
        }
        public Task<NPC> AddNPCAsync(NPC npc) => _npcService.AddNPCAsync(npc);
        public Task<NPC> GetNPCByIdAsync(int id) => _npcService.GetNPCByIdAsync(id);
        public Task<IEnumerable<NPC>> GetAllNPCsAsync() => _npcService.GetAllNPCsAsync();
        public Task<IReadOnlyList<NPC>> GetAllNPCsReadOnlyAsync() => _npcService.GetAllNPCsReadOnlyAsync();
        public async Task DeleteNPCAsync(int id) 
        {
            var npc = await _npcService.GetNPCByIdAsync(id);
            if (npc != null)
            {
                await _baseStatsService.DeleteBaseStatsAsync(npc.StatsId);
                await _npcService.DeleteNPCAsync(id);
            }
        }
        public Task<int> SaveChangesAsync() => _npcService.SaveChangesAsync();
        public Task<IEnumerable<NPC>> GetAllNPCsIncluding() => _npcService.GetAllNPCsIncluding();
       
    }
}
