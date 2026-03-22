using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppECommerce.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [MaxLength(150)]
        public string ProductName { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? Description { get; set; }
        [Precision(18, 2)]
        public decimal? Price { get; set; }
        public int StockQty { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? CreatedDT { get; set; } = DateTime.Now;
        public Category? Category { get; set; }
    }
}
