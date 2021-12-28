using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.DAL.Context;
using WebStore.Services;
//using WebStore.Services.InMemory;
using WebStore.Services.Interfaces;
using WebStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebStore.Services.InSQL;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddDbContext<WebStoreDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
services.AddTransient<IDbInitializer, DbInitializer>();
//services.AddSingleton<IEmployeeData, InMemoryEmployeesData>();
//services.AddSingleton<IProductData, InMemoryProductData>();
services.AddScoped<IProductData, SQLProductData>();
services.AddScoped<IEmployeeData, SQLEmployeesData>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));

app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<TestMiddleware>();

app.UseWelcomePage("/welcome");

 //app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await using (var scope = app.Services.CreateAsyncScope())
{
    var db_initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await db_initializer.InitializeAsync(RemoveBefore: true);
}

app.Run();