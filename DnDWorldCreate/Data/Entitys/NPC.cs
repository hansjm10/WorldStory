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
        public NPC()
        {
            Stats = new BaseStats();
        }

        /// <summary>
        /// Clones the NPC object.
        /// </summary>
        /// <returns>A cloned NPC object.</returns>
        public override BaseEntity Clone()
        {
            NPC clonedNpc = (NPC)base.Clone();
            clonedNpc.Name = Name;
            clonedNpc.Backstory = Backstory;
            clonedNpc.TownId = TownId;
            clonedNpc.ImagePath = ImagePath;
            clonedNpc.Alignment = Alignment;
            clonedNpc.Age = Age;

            clonedNpc.PersonalityTraits = new List<string>(this.PersonalityTraits);

            if (Stats != null)
            {
                clonedNpc.Stats = (BaseStats)Stats.Clone();
            }

            if (Town != null)
            {
                clonedNpc.Town = (Town)Town.Clone();
            }

            return clonedNpc;
        }
    }
}