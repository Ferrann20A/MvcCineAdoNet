using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme,
    config =>
    {
        config.AccessDeniedPath = "/Managed/AccesoDenegado";
    });

builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<IRepositoryCineBook, RepositoryCineBook>();
string connectionString = builder.Configuration.GetConnectionString("SqlServerCineBook");
builder.Services.AddDbContext<CineBookContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false);

var app = builder.Build();


//app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}"
            );
});

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
