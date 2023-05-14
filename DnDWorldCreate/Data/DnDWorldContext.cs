using Microsoft.EntityFrameworkCore;
using DnDWorldCreate.Data.Entitys;
using DnDWorldCreate.Data.Stats;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DnDWorldCreate.Data
{
    public class DnDWorldContext : DbContext
    {
        public DnDWorldContext(DbContextOptions<DnDWorldContext> options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<NPC> NPCs { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<BaseStats> BaseStats { get; set; }
        public DbSet<Building> Buildings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the NPC entity
            modelBuilder.Entity<NPC>(entity =>
            {
                // Set up the value conversion and value comparer for the PersonalityTraits property
                var propertyBuilder = entity.Property(e => e.PersonalityTraits)
                    .HasConversion(
                        v => string.Join(";", v), // Serialize the list as a string
                        v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList() // Deserialize the string as a list
                    )
                    .IsUnicode(false);

                propertyBuilder.Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2), // Check if two lists are equal
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), // Calculate the hash code for a list
                    c => c.ToList() // Create a shallow copy of the list
                ));

                // You can also configure other properties, such as setting column types or setting up relationships
            });
        }



    }
}
