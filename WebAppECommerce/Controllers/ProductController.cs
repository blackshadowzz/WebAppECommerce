using Microsoft.AspNetCore.Mvc;

namespace WebAppECommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
