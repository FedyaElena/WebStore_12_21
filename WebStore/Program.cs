using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.DAL.Context;
using WebStore.Services;
using WebStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllersWithViews(opt =>
{
    opt.Conventions.Add(new TestConvention());
});

services.AddDbContext<WebStoreDB>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
services.AddTransient<IDbInitializer, DbInitializer>();
services.AddSingleton<IEmployeeData, InMemoryEmployeesData>();
services.AddSingleton<IProductData, InMemoryProductData>();


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
    await db_initializer.InitializeAsync(RemoveBefore: false);
}

app.Run();