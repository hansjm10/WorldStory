namespace DnDWorldCreate.Data.Entitys
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Creates a shallow copy of the current BaseEntity object.
        /// </summary>
        /// <returns>A shallow copy of the current BaseEntity object.</returns>
        public virtual BaseEntity Clone()
        {
            return (BaseEntity)MemberwiseClone();
        }

    }
}
