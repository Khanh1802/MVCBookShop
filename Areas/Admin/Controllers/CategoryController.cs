using BookShopWeb.DataAccess.Repositories;
using BookShopWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
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
                _categoryRepository.Add(create);
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
            var category = await _categoryRepository.GetByIdAsync(id); //C1
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
                _categoryRepository.Update(category);
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
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Delete(category);
            return RedirectToAction("Index", "Category");
        }
    }
}
