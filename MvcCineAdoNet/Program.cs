using MvcCineAdoNet.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
});

//builder.Services.AddTransient<IRepositoryCine, RepositoryCineSQLServer>();


var app = builder.Build();


//app.MapGet("/", () => "Hello World!");

app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();
