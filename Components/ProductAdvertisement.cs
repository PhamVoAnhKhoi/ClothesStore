using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Components
{
    public class ProductAdvertisementViewComponent : ViewComponent
    {
        private readonly IStoreRepository repository;

        public ProductAdvertisementViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke(string productName)
        {
            var product = repository.Products
                .FirstOrDefault(p => p.Name == productName);
            if (product == null)
            {
                return Content("Product not found");
            }    

            //ViewData["ProductName"] = productName;

            return View(product);
        }
    }
}