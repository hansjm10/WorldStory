using DnDWorldCreate.Data;
using DnDWorldCreateTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal;

public class TestDnDWorldContext : DnDWorldContext
{
    public TestDnDWorldContext(DbContextOptions<TestDnDWorldContext> options)
        : base(CreateDnDWorldContextOptions(options))
    {
    }

    public DbSet<TestEntity> TestEntities { get; set; }

    private static DbContextOptions<DnDWorldContext> CreateDnDWorldContextOptions(DbContextOptions<TestDnDWorldContext> options)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DnDWorldContext>();
        optionsBuilder.UseInMemoryDatabase(options.FindExtension<InMemoryOptionsExtension>().StoreName);
        return optionsBuilder.Options;
    }
}
