using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppECommerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Length(5, 100)]
        [Column(TypeName = "nvarchar(100)")]
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
