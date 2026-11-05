using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;
using WebAppECommerce.Models;

namespace WebAppECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.OrderLines).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> ListOrdersAsync(string? userId = null, string? filter = null)
        {
            var query = _context.Orders.Include(o => o.OrderLines).AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(o => o.UserId == userId);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(o => o.OrderNumber.ToString()!.Contains(filter));
            }

            return await query.ToListAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }

}
