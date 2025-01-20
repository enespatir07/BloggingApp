using Microsoft.EntityFrameworkCore;

namespace BloggingApp.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }  // If not using ASP.NET Identity directly.
    }
}