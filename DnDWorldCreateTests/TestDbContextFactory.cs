using DnDWorldCreate.Data;
using Microsoft.EntityFrameworkCore;

public class TestDbContextFactory : IDbContextFactory<DnDWorldContext>
{
    private readonly DbContextOptions<TestDnDWorldContext> _options;

    public TestDbContextFactory(DbContextOptions<TestDnDWorldContext> options)
    {
        _options = options;
    }

    public DnDWorldContext CreateDbContext()
    {
        return new TestDnDWorldContext(_options);
    }
}
