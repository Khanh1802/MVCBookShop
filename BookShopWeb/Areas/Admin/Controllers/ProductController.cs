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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> CreateOrUpdate(Guid? id)
        {
            //Key is Categories and value is categories
            //ViewBag.Categories = categories;
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
            if (id == null || id == Guid.Empty)
            {
                //create

                return View(productVM);
            }
            else
            {
                //update

                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                productVM.Product = product;
                return View(productVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(ProductVM productVM, IFormFile? file)
        {
            ModelState.Remove("Product.Category");
            ModelState.Remove("Product.ImageUrl");
            ModelState.Remove("Categories");
            if (ModelState.IsValid)
            {
                //take root folder which is wwwRootFolder 
                string wwwRootPath = _webHostEnvironment.WebRootPath; //using DI provide by Dotnet
                if (file != null)
                {
                    //take extension name file
                    string extensionFile = Path.GetExtension(file.FileName);

                    //create name file
                    string fileName = Guid.NewGuid().ToString() + extensionFile;

                    //navigate to the product path
                    //go inside product folder
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    //Combine productPath and file name => stream pathn ultimate location
                    string streamPath = Path.Combine(productPath, fileName);
                    //Save into product folder
                    using (var fileStream = new FileStream
                        (streamPath,
                        /*Bc create new file so =>*/FileMode.Create))
                    {
                        //Copies the contents of the uploaded file to the target stream.
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = $@"\images\product\{fileName}";
                }
                //Create
                if (productVM.Product.Id == null || productVM.Product.Id == Guid.Empty)
                {
                    await _productRepository.AddAsync(productVM.Product);
                    return RedirectToAction("Index", "Product");
                }
                //Update
                else
                {
                    await _productRepository.UpdateAsync(productVM.Product);
                    return RedirectToAction("Index", "Product");
                }
            }
            productVM.Categories =
                (await _categoryRepository.GetAllAsync())
                .Select(x => new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            return View(productVM);
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
            await _productRepository.DeleteAsync(product);
            return RedirectToAction("Index", "Product");
        }
    }
}
