using System.IO.Pipes;

namespace DnDWorldCreate.Data.Entitys
{
    public class Town : BaseEntity
    {
        public string? Name { get; set; }
        public Region? Region { get; set; }
        public int RegionId { get; set; }
        public List<NPC> NPCs { get; set; } = new List<NPC>();
        /// <summary>
        /// Creates a clone of the Town object.
        /// </summary>
        /// <returns>A clone of the Town object.</returns>
        public override BaseEntity Clone()
        {
            Town clonedTown = (Town)base.Clone();

            clonedTown.Name = Name;
            clonedTown.RegionId = RegionId;

            //If the Region is not null, clone the Region and assign it to the clonedTown
            if (Region != null)
            {
                clonedTown.Region = (Region)Region.Clone();
            }

            //Create a new list of NPCs for the clonedTown
            clonedTown.NPCs = new List<NPC>();

            //Loop through the NPCs in the original town
            foreach (NPC npc in NPCs)
            {
                //If the NPC is not null, clone it and add it to the clonedTown's list of NPCs
                if (npc != null)
                {
                    clonedTown.NPCs.Add((NPC)npc.Clone());
                }
            }

            return clonedTown;
        }


    }
}