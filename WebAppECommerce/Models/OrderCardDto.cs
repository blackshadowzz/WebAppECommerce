namespace WebAppECommerce.Models
{
    public class OrderCardDto
    {
        public required Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal? SubTotal { get; set; }

    }
}
