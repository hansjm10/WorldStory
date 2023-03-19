namespace DnDWorldCreate.Data
{
    public class Region : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Town> Towns { get; set; } = new List<Town>();
    }
}
