using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IRepositoryCineBook, RepositoryCineBook>();
string connectionString = builder.Configuration.GetConnectionString("SqlServerCineBook");
builder.Services.AddDbContext<CineBookContext>
    (options => options.UseSqlServer(connectionString));


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});


var app = builder.Build();


//app.MapGet("/", () => "Hello World!");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
