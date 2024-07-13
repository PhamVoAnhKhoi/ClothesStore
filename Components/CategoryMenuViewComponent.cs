using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryRepository repository;

        public CategoryMenuViewComponent(ICategoryRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {          
            var categories = repository.Categories
            .OrderByDescending(p => p.CategoryID)
            .Take(6)
            .ToList();
            return View(categories);
        }
    }
}
