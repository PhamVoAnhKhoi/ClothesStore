using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Components
{
    public class AllProductViewComponent : ViewComponent
    {
        private readonly IStoreRepository repository;

        public AllProductViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

         public IViewComponentResult Invoke()
        {
            var products = repository.Products
                .OrderByDescending(p => p.ProductID)
                .Take(6)
                .ToList();

            return View(products);
        }
    }
}
