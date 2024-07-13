using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models.ViewModels;
using ClothesStore.Models; 

namespace ClothesStore.Controllers 
{ 
    public class HomeController : Controller 
    { 
        private IStoreRepository repository; 
        //Số lượng sản phẩm được hiển thị mỗi page
        public int PageSize = 3;
        public HomeController(IStoreRepository repo) 
        { 
            repository = repo; 
        } 
       
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
