using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models.ViewModels;
using ClothesStore.Models; 

namespace ClothesStore.Controllers 
{ 
    public class CategoryController : Controller 
    { 
        private ICategoryRepository repository; 
        public CategoryController(ICategoryRepository repo) 
        { 
            repository = repo; 
        } 
       
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
