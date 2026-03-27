using Microsoft.AspNetCore.Mvc;
using WebAppECommerce.Models;

namespace WebAppECommerce.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(int _age)
        {
            this.num = _age;
        }
        public int num { get; set; }
        private int _age;

        public ViewResult Index()
        {
            //null num = 100;
            Console.WriteLine("Testing...");
            Console.WriteLine(_age);
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
