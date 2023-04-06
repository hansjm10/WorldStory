using DnDWorldCreate.Data;
using DnDWorldCreate.Services;
using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Services.Repositories;
using Microsoft.EntityFrameworkCore;

public static class ServiceConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddDbContextFactory<DnDWorldContext>(options => options.UseSqlServer(configuration.GetConnectionString("MyDatabase")));

        services.AddScoped<RegionService>();
        services.AddScoped<TownService>();
        services.AddScoped<NPCService>();
        services.AddScoped<RegionManagerService>();
        services.AddScoped<ItemService>();
        services.AddScoped<BaseStatsService>();
        services.AddScoped<NPCManagerService>();
        services.AddScoped<INPCServiceBase, NPCManagerService>();
        services.AddScoped<ITownRepository, TownRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }
}
