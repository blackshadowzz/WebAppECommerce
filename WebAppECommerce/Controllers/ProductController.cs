using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppECommerce.Data;

namespace WebAppECommerce.Controllers
{
    public class ProductController(AppDbContext dbContext, IWebHostEnvironment _env) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {

            //ViewBag 
            ViewBag.Categories = new SelectList(dbContext.Categories.ToList(), "Id", "CategoryName");

            ViewData["Data"] = new List<string> { "Data 1", "Data 2", "Data 3" };

            var categories = dbContext.Categories.ToList();
            TempData["Message"] = "This is a message from TempData.";
            return View(categories);
        }
    }
}
