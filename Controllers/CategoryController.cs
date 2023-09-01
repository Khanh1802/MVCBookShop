using BookShopWeb.DataAccess.Data;
using BookShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShopWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        //View phải trùng với tên action method
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category create)
        {
            if (ModelState.IsValid)
            {
                await _context.AddAsync(create);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public async Task<IActionResult> Update(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id); //C1
            //var category2 = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id); //C2
            //var category3 = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync(); //C3
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id); //C1
            //var category2 = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id); //C2
            //var category3 = await _context.Categories.Where(x => x.Id == id).FirstOrDefaultAsync(); //C3
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Category");
        }
    }
}
