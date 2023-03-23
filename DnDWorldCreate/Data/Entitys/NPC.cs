namespace DnDWorldCreate.Data.Entitys
{
    public class NPC : BaseEntity
    {
        public string? Name { get; set; }
        public string? Occupation { get; set; }
        public string? Backstory { get; set; }
        public Town? Town { get; set; }
        public int TownId { get; set; }
    }
}