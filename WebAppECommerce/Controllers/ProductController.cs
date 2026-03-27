using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;
using WebAppECommerce.Models;

namespace WebAppECommerce.Controllers
{
    public class ProductController(AppDbContext dbContext, IWebHostEnvironment _env) : Controller
    {

        //List product
        public async Task<ActionResult<List<Product>>> Index(string? search = "")
        {
            var products = dbContext.Products.Include(x => x.Category).AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(x =>
                    EF.Functions.Like(x.ProductName, $"%{search}%") ||
                    EF.Functions.Like(x.Description, $"%{search}%"));
            }

            return View(await products.ToListAsync());
        }

        // show details view
        public async Task<IActionResult> Details(int id)
        {
            var result = await dbContext.Products
                .AsNoTracking()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.ProductId == id);
            return View(result);
        }

        // Show product view Create.cshtml
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                dbContext.Categories.ToList(),
                "Id", "CategoryName"
                );
            return View();
        }

        // For upload image
        [NonAction]
        private async Task<string?> GetImagePath(IFormFile? file)
        {
            string folder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid() + Path.GetExtension(file?.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file!.CopyToAsync(stream);
            }
            return "/images/" + fileName;
        }

        //Insert product to database
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Product product)
        {

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    product.ImageUrl = await GetImagePath(product.ImageFile);
                }
                dbContext.Products.Add(product);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //Show edit view
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(
                dbContext.Categories.ToList(),
                "Id", "CategoryName"
                );
            var result = await dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == id);
            return View(result);
        }

        //Edit product
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {

                    product.ImageUrl = await GetImagePath(product.ImageFile);
                }
                dbContext.Products.Update(product);
                await dbContext.SaveChangesAsync();
                TempData["Success"] = "Product created successfully";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Delete method
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var product = dbContext.Products.FirstOrDefault(x => x.ProductId == id);
                    dbContext.Products.Remove(product!);
                    await dbContext.SaveChangesAsync();
                    TempData["Success"] = "Product deleted successfully";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["Success"] = ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
