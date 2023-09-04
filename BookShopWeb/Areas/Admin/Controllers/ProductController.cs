using BookShopWeb.DataAccess.Repositories;
using BookShopWeb.DataAccess.ViewModels;
using BookShopWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductController(IProductRepository productRepository
            , ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var productVM = new ProductVM()
            {
                Categories =
                (await _categoryRepository.GetAllAsync())
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new Product()
            };

            //Key is Categories and value is categories
            //ViewBag.Categories = categories;
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM)
        {
            ModelState.Remove("Product.Category");
            ModelState.Remove("Product.ImageUrl");
            ModelState.Remove("Categories");
            if (ModelState.IsValid)
            {
                _productRepository.Add(productVM.Product);
                return RedirectToAction("Index", "Product");
            }
            productVM.Categories =
                (await _categoryRepository.GetAllAsync())
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            //Key is Categories and value is categories
            //ViewBag.Categories = categories;
            return View(productVM);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            _productRepository.Delete(product);
            return RedirectToAction("Index", "Product");
        }
    }
}
