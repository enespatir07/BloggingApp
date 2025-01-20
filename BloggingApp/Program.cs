using Microsoft.EntityFrameworkCore;
using BloggingApp.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.
builder.Services.AddControllersWithViews();

// 2. Configure EF Core with MySQL
builder.Services.AddDbContext<BloggingContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 30))
    );
});

// 3. Add support for caching and sessions (for simple login/session tracking)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Session settings (optional)
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. Build the application
var app = builder.Build();

// 5. Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session before authorization so session data is available
app.UseSession();

app.UseAuthorization();

// 6. Configure routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

// 7. Run
app.Run();