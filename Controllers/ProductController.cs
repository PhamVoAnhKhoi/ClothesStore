using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;
using ClothesStore.Models.ViewModels;

namespace ClothesStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IStoreRepository repository;
        public int PageSize = 12;

        public ProductController(IStoreRepository repo)
        {
            repository = repo;
        }

        // public IActionResult Index()
        // {
        //     var products = repository.Products.ToList();
        //     return View(products);
        // }

        public ViewResult Index(int productPage = 1) 
        => View(new ProductsListViewModel { 
            Products = repository.Products 
            .OrderBy(p => p.ProductID) 
            .Skip((productPage - 1) * PageSize) 
            .Take(PageSize), PagingInfo = new PagingInfo {
                CurrentPage = productPage, 
                ItemsPerPage = PageSize, 
                TotalItems = repository.Products.Count() 
            } 
        });
        public string About()
        {
            return "abc";
        }

        public IActionResult ProductOfEachCategory(string category){
            var categoryId = repository.Categories.FirstOrDefault(c => c.CategoryName == category)?.CategoryID;
            if (categoryId == null)
            {
                // Trường hợp không tìm thấy danh mục, có thể xử lý thông báo lỗi ở đây
                return Content("Category not found");
            }
             var products = repository.Products
                .Where(p => p.CategoryID == categoryId)
                .ToList();
            ViewData["Category"] = category;
            return View(products);
        }
        // Thêm hành động tìm kiếm
        [HttpPost]
        public IActionResult Search(string searchTerm)
        {
            var products = repository.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToList();
            ViewData["SearchTerm"] = searchTerm;
            return View(products);
        }
    }
}
