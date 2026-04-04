using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;
using WebAppECommerce.Models;

namespace WebAppECommerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext dbContext;

        public OrderService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Order order)
        {
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid orderId)
        {
            var order = await dbContext.Orders
                .FirstOrDefaultAsync(x => x.OrderId == orderId);

            dbContext.Orders.Remove(order!);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string? UserId = "", string? filter = null)
        {
            var queries = dbContext.Orders.Include(x => x.OrderLines).AsNoTracking();
            if (!string.IsNullOrEmpty(UserId))
            {
                queries = queries.Where(x => x.UserId == UserId);
            }
            if (!string.IsNullOrEmpty(filter))
            {
                queries = queries.Where(x => x.OrderNumber.ToString()!.Contains(filter));
            }
            return await queries.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            return await dbContext.Orders.Include(x => x.OrderLines)
                .FirstOrDefaultAsync(x => x.OrderId == orderId) ?? new Order();
        }


        public async Task UpdateAsync(Order order)
        {
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
