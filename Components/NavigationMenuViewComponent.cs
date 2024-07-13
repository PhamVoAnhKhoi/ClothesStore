using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
namespace ClothesStore.Components 
{ 
    public class NavigationMenuViewComponent : ViewComponent 
    { 
        private IStoreRepository repository;
        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke() 
        { 
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
                    .Select(x=>x.CategoryName)
                    .Distinct()
                    .OrderBy(x=>x));
        } 
    } 
}