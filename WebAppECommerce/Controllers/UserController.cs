using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;

namespace WebAppECommerce.Controllers
{
    public class UserController(AppDbContext dbContext) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                TempData["Error"] = "Invalid username or password.";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home", new { id = user?.Id });
        }
    }
}
