using System.IO.Pipes;

namespace DnDWorldCreate.Data.Entitys
{
    public class Town : BaseEntity
    {
        public string? Name { get; set; }
        public Region? Region { get; set; }
        public int RegionId { get; set; }
        public List<NPC> NPCs { get; set; } = new List<NPC>();
    }
}