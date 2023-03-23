using DnDWorldCreate.Data.Interfaces;

namespace DnDWorldCreate.Data.Entitys
{
    public class Region : BaseEntity, IImageEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Town> Towns { get; set; } = new List<Town>();
        public string? ImagePath { get; set; }
    }
}
