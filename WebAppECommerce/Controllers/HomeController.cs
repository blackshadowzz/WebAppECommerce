using Microsoft.AspNetCore.Mvc;

namespace WebAppECommerce.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet] //GET, POST, PUT, PATCH, DELETE
        public IActionResult Index()
        {

            return View();
        }

        //public, private, internal... => access modifiers
        //void 

        [HttpPost]
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
