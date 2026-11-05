using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;
using WebAppECommerce.Models;

namespace WebAppECommerce.Controllers
{
    public class HomeController(AppDbContext dbContext) : Controller
    {
        [HttpGet] //GET, POST, PUT, PATCH, DELETE
        public async Task<ViewResult> Index()
        {
            // id of User is passed from UserController to HomeController
            var userId = HttpContext.Request.Query["id"].ToString();
            if (!string.IsNullOrEmpty(userId))
            {
                ViewData["UserId"] = userId;
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
                if (user != null)
                {
                    ViewData["Username"] = user.Username;
                }
            }
            return View();
        }
        //public, private, internal... => access modifiers
        //void 
        public EmptyResult LogAction()
        {
            Console.WriteLine("This Return type EmptyResult");
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return RedirectToAction("Details");
        }
        public ViewResult Details()
        {
            var student = new Student
            {
                Id = 1,
                Name = "Sophea",
                Age = 20
            };

            return View(student);
        }

    }
}
