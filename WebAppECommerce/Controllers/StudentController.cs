using Microsoft.AspNetCore.Mvc;
using WebAppECommerce.Models;

namespace WebAppECommerce.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = new List<Student>()
            {
                new Student() {Id=1, Name="Bala ", Age=20 },
                new Student() {Id=2, Name="Bala 1", Age=20 },
                new Student() {Id=3, Name="Bala 2", Age=20 },
                new Student() {Id=4, Name="Bala 3", Age=20 }
            };
            return View(students.ToList());
        }

        public IActionResult Create()
        {

            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Report()
        {
            return View("Reports/Report");
        }

    }
}
