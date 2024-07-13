using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Components
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IStoreRepository repository;

        public ProductCategoryViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke(string category)
        {
            var categoryId = repository.Categories.FirstOrDefault(c => c.CategoryName == category)?.CategoryID;
            if (categoryId == null)
            {
                // Trường hợp không tìm thấy danh mục, có thể xử lý thông báo lỗi ở đây
                return Content("Category not found");
            }
             var products = repository.Products
                .Where(p => p.CategoryID == categoryId)
                .Take(6)
                .ToList();

            ViewData["Category"] = category;

            return View(products);
        }
    }
}
