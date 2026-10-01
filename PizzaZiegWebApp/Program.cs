using Microsoft.AspNetCore.Identity;
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


//настраиваем Identity систему (для админа)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//настраиваем Auth cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "myCompanyAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/admin/accessdenied";
    options.SlidingExpiration = true;
});


builder.Services.AddControllersWithViews();


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
