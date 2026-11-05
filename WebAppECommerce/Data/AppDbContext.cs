using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Models;
namespace WebAppECommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Student> Students { get; set; } = default!;

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationships between Order and OrderLine
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne(ol => ol.Order)
                .HasForeignKey(ol => ol.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
