using Microsoft.AspNetCore.Mvc;
using WebAppECommerce.Models;

namespace WebAppECommerce.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index(string? search)
        {
            List<string> students = new List<string>()
            {
                "John",
                "Jane",
                "Jack",
                "Jill"
            };
            students = students.Where(

                x => x.ToLower().Contains(search?.ToLower() ?? string.Empty)

                ).ToList();

            return Ok(students.ToList());
        }

        public IActionResult Details(int age)
        {
            return Ok(age);
        }

        public IActionResult Edit(Product product)
        {
            return Ok(product);
        }
    }
}
