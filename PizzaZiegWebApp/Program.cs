using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PizzaZiegWebApp.Domain;
using PizzaZiegWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

IConfigurationBuilder configBuild = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


var configuration = configBuild.Build();
var config = configuration.GetSection("Project").Get<AppConfig>();

ArgumentNullException.ThrowIfNull(config, "config");

builder.Services.AddDbContext<AppDbContext>
    (x => x.UseLazyLoadingProxies().UseSqlServer(config.Database.ConnectionString).
    ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning)));


builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
