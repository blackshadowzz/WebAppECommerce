using WebAppECommerce.Models;

namespace WebAppECommerce.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync(string? UserId = "", string? filter = null);
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid orderId);
    }
}
