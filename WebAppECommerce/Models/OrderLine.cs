namespace WebAppECommerce.Models
{
    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; } // Reference to the Order
        public int? ProductId { get; set; } // Reference to the Product
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public Order? Order { get; set; }
    }
}
