using DnDWorldCreate.Data;
using Microsoft.EntityFrameworkCore;

public class TestDbContextFactory : IDbContextFactory<DnDWorldContext>
{
    private readonly DbContextOptions<DnDWorldContext> _options;

    public TestDbContextFactory(DbContextOptions<DnDWorldContext> options)
    {
        _options = options;
    }

    public DnDWorldContext CreateDbContext()
    {
        return new DnDWorldContext(_options);
    }
}
