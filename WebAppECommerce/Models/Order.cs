namespace WebAppECommerce.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public int? OrderNumber { get; set; }
        public string? UserId { get; set; }
        public int? TotalItem { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; } = OrderStatus.Pending.ToString();
        public string? ShippingAddress { get; set; }
        public DateTime? OrderDate { get; set; } = DateTime.Now;
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
