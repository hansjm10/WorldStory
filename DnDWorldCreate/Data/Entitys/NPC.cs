using DnDWorldCreate.Data.Interfaces;
using DnDWorldCreate.Data.Stats;
using DnDWorldCreate.Data.Enums;
namespace DnDWorldCreate.Data.Entitys
{
    public class NPC : BaseEntity, IImageEntity
    {
        public string? Name { get; set; }
        public string? Occupation { get; set; }
        public string? Backstory { get; set; }
        public Town? Town { get; set; }
        public int TownId { get; set; }
        public string? ImagePath { get; set; }
        public int StatsId { get; set; }
        public BaseStats Stats { get; set; }
        public Alignment? Alignment { get; set; }
        public int? Age { get; set; }
        //public List<Item> Inventory { get; set; } = new List<Item>();
        public List<string> PersonalityTraits { get; set; } = new List<string>();
        public NPC() { 
            Stats = new BaseStats();
        }
    }
}