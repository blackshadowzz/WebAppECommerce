using Microsoft.AspNetCore.Mvc;

namespace WebAppECommerce.Controllers
{
    public class ProductController : Controller
    {

        [ActionName("Edit")]
        public IActionResult Index()
        {
            HelperMethod();
            return View();
        }

        [NonAction]
        public void HelperMethod()
        {
            Console.WriteLine("Non Action");
            Console.WriteLine("Hello");
        }

        [NonAction]
        public int CalculateDiscount(int total)
        {
            return total > 100 ? 10 : 0;
        }

        public IActionResult Checkout()
        {
            int discount = CalculateDiscount(150);
            Console.WriteLine($"Discount: {discount}");
            Console.WriteLine(discount);
            return View(discount);
        }
    }
}
