using WebAppECommerce.Models;

namespace WebAppECommerce.Services
{
    public interface IOrderService
    {
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>> ListOrdersAsync(string? userId = null, string? filter = null);
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid id);
    }

}
