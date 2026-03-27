using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Models;
namespace WebAppECommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<WebAppECommerce.Models.Student> Student { get; set; } = default!;
    }
}
