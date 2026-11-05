

using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppECommerce.Models
{
    public class Customer
    {
        //Member: Properties or Method
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(150)")] //column lenth / datatype
        public string CustomerName { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(100)")]
        public string? FirstName { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string? LastName { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string? Gender { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? Phone { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
