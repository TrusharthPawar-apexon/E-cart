using Ecart.Models;
using Ecart.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecart.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = productRepository.GetProducts();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        // Action to Create Product in database.
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                productRepository.CreateProduct(product);
                ViewBag.Message = "Created";
                return View("Success");
            }
            return View(product);
        }

        public IActionResult Edit(Guid Id) 
        {
            Product product = productRepository.GetProduct(Id);
            if(product == null)
            {
                return View("NotFound");
            }
            return View(product);
        }

        // Action to update product details.
        [HttpPost]
        public IActionResult Edit(Product product) 
        {
            productRepository.UpdateProduct(product);
            ViewBag.Message = "Updated";
            return View("Success");
        }

        public IActionResult Delete(Guid Id)
        {
            var product = productRepository.GetProduct(Id);
            if(product == null)
            {
                return View("NotFound");
            }
            return View(product);
        }

        // Action to delete product from database
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(Guid Id)
        {
            productRepository.DeleteProduct(Id);
            ViewBag.Message = "Deleted";
            return View("Success");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
