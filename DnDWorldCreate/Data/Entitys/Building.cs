namespace DnDWorldCreate.Data.Entitys
{
    public class Building : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }

        public override BaseEntity Clone()
        {
            Building clonedBuilding = (Building)base.Clone();

            clonedBuilding.Name = Name;
            clonedBuilding.Description = Description;
            clonedBuilding.Type = Type;
            clonedBuilding.Material = Material;
            clonedBuilding.Size = Size;

            return clonedBuilding;

        }
    }
}
