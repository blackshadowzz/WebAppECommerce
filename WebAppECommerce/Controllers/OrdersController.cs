using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAppECommerce.Data;
using WebAppECommerce.Models;
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


        public IActionResult Index(string? filter = null)
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
            ViewBag.Cart = GetCartItems();

            return View();
        }

        private List<OrderCardDto> GetCartItems()
        {
            // Get the item IDs from the cart stored in the session
            var cartItemIds = GetCartItemIds();
            // Retrieve the corresponding items from the database
            var cartItems = dbContext.Products
                .Where(item => cartItemIds.Contains(item.ProductId))
                .ToList();

            // Create a list of OrderCardDto objects to represent
            // the items in the cart along with
            // their quantities and subtotals
            return cartItems
                .Select(item => new OrderCardDto
                {
                    Product = item,
                    Quantity = cartItemIds.Count(id => id == item.ProductId),
                    SubTotal = item.Price * cartItemIds.Count(id => id == item.ProductId)
                })
                .OrderBy(line => line.Product.ProductName)
                .ToList();
        }

        private List<int> GetCartItemIds()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);

            if (string.IsNullOrWhiteSpace(cartJson))
            {
                return [];
            }
            return JsonSerializer.Deserialize<List<int>>(cartJson) ?? [];
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int itemId)
        {
            // Get the current cart item IDs from the session,
            // add the new item ID, and save it back to the session
            var cartItemIds = GetCartItemIds();
            cartItemIds.Add(itemId);
            // Save the updated cart item IDs back to the session
            SaveCartItemIds(cartItemIds);

            return RedirectToAction(nameof(Index));
        }
        private void SaveCartItemIds(List<int> cartItemIds)
        {
            var cartJson = JsonSerializer.Serialize(cartItemIds);
            HttpContext.Session.SetString(CartSessionKey, cartJson);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int itemId)
        {
            // Get the current cart item IDs from the session,
            // remove the specified item ID, and save it back to the session
            var cartItemIds = GetCartItemIds();
            cartItemIds.Remove(itemId);
            // Save the updated cart item IDs back to the session
            SaveCartItemIds(cartItemIds);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearCart()
        {
            // Clear the cart by removing the session key
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrder(Order orderInput)
        {
            //Check if the cart is empty before saving the order
            var cartItemIds = GetCartItemIds();
            if (cartItemIds.Count == 0)
            {
                TempData["OrderError"] = "Cart is empty.";
                return RedirectToAction(nameof(Index));
            }

            // Create a new Order object and populate it with the order details
            var cardItems = GetCartItems();

            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderNumber = (dbContext.Orders.Max(o => (int?)o.OrderNumber) ?? 0) + 1,
                TotalItem = cardItems.Count,
                UserId = orderInput.UserId,
                ShippingAddress = orderInput.ShippingAddress,
                Status = string.IsNullOrWhiteSpace(orderInput.Status) ? "Pending" : orderInput.Status
            };

            // Create OrderLine objects for each item in the cart
            // and calculate the total amount
            order.OrderLines = cardItems.Select(ci => new OrderLine
            {
                ProductId = ci.Product.ProductId,
                UnitPrice = ci.Product.Price,
                Quantity = ci.Quantity,
                SubTotal = ci.SubTotal
            }).ToList();

            order.TotalAmount = order.OrderLines.Sum(line => line.SubTotal ?? 0m);

            await _orderService.CreateAsync(order);

            HttpContext.Session.Remove(CartSessionKey);
            TempData["OrderSuccess"] = $"Order #{order.OrderNumber} saved.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> List(string? userId = null, string? filter = null)
        {
            var orders = await _orderService.GetAllOrdersAsync(userId, filter);

            return View(orders);
        }
    }
}
