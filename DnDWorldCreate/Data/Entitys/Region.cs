using DnDWorldCreate.Data.Interfaces;

namespace DnDWorldCreate.Data.Entitys
{
    public class Region : BaseEntity, IImageEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Town> Towns { get; set; } = new List<Town>();
        public string? ImagePath { get; set; }

        /// <summary>
        /// Creates a clone of the current Region object.
        /// </summary>
        /// <returns>A clone of the current Region object.</returns>
        public override BaseEntity Clone()
        {
            Region clonedRegion = (Region)base.Clone();
            clonedRegion.Name = Name;
            clonedRegion.Description = Description;
            clonedRegion.ImagePath = ImagePath;

            clonedRegion.Towns = new List<Town>();
            foreach (var town in Towns)
            {
                if (town != null)
                {
                    clonedRegion.Towns.Add((Town)town.Clone());
                }
            }

            return clonedRegion;
        }
    }
}
