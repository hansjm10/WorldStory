namespace DnDWorldCreate.Data.Entitys
{
    public class Item : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public override BaseEntity Clone()
        {
            Item clonedItem = (Item)base.Clone();

            clonedItem.Name = Name;
            clonedItem.Description = Description;

            return clonedItem;
        }
    }
}
