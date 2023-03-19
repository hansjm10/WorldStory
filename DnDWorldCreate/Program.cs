using DnDWorldCreate.Data;
using DnDWorldCreate.Services;
using DnDWorldCreate.Services.Interfaces;
using DnDWorldCreate.Services.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<DnDWorldContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")), ServiceLifetime.Transient);
builder.Services.AddScoped(typeof(RegionService));
builder.Services.AddScoped(typeof(TownService));
builder.Services.AddScoped(typeof(NPCService));
builder.Services.AddScoped(typeof(RegionManagerService));
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
