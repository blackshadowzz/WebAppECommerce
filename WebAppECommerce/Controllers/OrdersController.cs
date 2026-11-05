using Microsoft.AspNetCore.Mvc;
using WebAppECommerce.Data;
using WebAppECommerce.Services;

namespace WebAppECommerce.Controllers
{
    public class OrdersController : Controller
    {
        private const string CartSessionKey = "CartItemIds";
        private readonly IOrderService _orderService;
        private readonly AppDbContext dbContext;

        public OrdersController(IOrderService orderService, AppDbContext dbContext)
        {
            _orderService = orderService;
            this.dbContext = dbContext;
        }

        public IActionResult Index(string? filter = "")
        {
            var keyword = filter?.Trim() ?? string.Empty;

            var items = dbContext.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                items = items.Where(x => x.ProductName.ToLower().Contains(keyword.ToLower()));
            }

            var sales = items
                .OrderBy(x => x.ProductName)
                .ToList();

            ViewBag.Filter = keyword;
            ViewBag.Sales = sales;
            //ViewBag.Cart = GetCartItems();

            return View();
        }


    }

}
