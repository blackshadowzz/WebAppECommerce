using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppECommerce.Data;
using WebAppECommerce.Models;

namespace WebAppECommerce.Controllers
{
    public class CategoryController : Controller
    {
        public readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<List<Category>>> Index(string? search)
        {
            var categories = _context.Categories.AsNoTracking();
            TempData["CurrentFilter"] = search;
            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => c.CategoryName.ToLower().Contains(search.ToLower()));
            }
            return View(await categories.ToListAsync());
        }
        public async Task<ActionResult<Category>> Details(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Store([Bind("CategoryName", "Description", "IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Category created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<ActionResult<Category>> Edit(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Category updated successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Categories.AnyAsync(x => x.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
