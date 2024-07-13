using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;

namespace ClothesStore.Controllers
{
    public class ProductDetailController : Controller
    {
        private IStoreRepository repository;

        public ProductDetailController(IStoreRepository repo)
        {
            repository = repo;
        }

        public IActionResult Detail(long productId)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
