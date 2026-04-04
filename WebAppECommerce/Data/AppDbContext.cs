using Microsoft.EntityFrameworkCore; // EF Core
using WebAppECommerce.Models;
namespace WebAppECommerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> TblCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; } = default!;

        // Order and OrderLine DbSet
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Order and OrderLine relationship
            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderLines)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);



            base.OnModelCreating(modelBuilder);
        }

    }
}
