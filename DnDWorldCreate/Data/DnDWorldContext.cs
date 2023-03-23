using Microsoft.EntityFrameworkCore;
using DnDWorldCreate.Data.Entitys;
namespace DnDWorldCreate.Data
{
    public class DnDWorldContext : DbContext
    {
        public DnDWorldContext(DbContextOptions<DnDWorldContext> options) : base(options) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<NPC> NPCs { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
