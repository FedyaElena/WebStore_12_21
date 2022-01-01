using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Middleware;
using WebStore.DAL.Context;
using WebStore.Services;
//using WebStore.Services.InMemory;
using WebStore.Services.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebStore.Services.InSQL;
using Microsoft.AspNetCore.Identity;

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

services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WebStoreDB>()
    .AddDefaultTokenProviders();

services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif
    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});

services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore.GB";
    opt.Cookie.HttpOnly = true;
    opt.Cookie.Expiration = TimeSpan.FromDays(10);
    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";
    opt.SlidingExpiration = true;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Map("/testpath", async context => await context.Response.WriteAsync("Test middleware"));

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

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