using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages; 
using ClothesStore.Infrastructure; 
using ClothesStore.Models;

namespace ClothesStore.Pages 
{ 
    public class CartModel : PageModel 
    { 
        private IStoreRepository repository; 
        public CartModel(IStoreRepository repo, Cart cartService) 
        { 
            repository = repo; 
            Cart = cartService;
        } 
        public Cart? Cart { get; set; } 
        public string ReturnUrl { get; set; } = "/"; 
        public void OnGet(string returnUrl) 
        { 
            ReturnUrl = returnUrl ?? "/"; 
        } 
        public IActionResult OnPost(long productId, string returnUrl) 
        { 
            Product? product = repository.Products.FirstOrDefault(p => p.ProductID == productId); 
            if (product != null) 
            { 
                Cart.AddItem(product, 1);
            }
            return Page(); // Trả về trang hiện tại mà không chuyển hướng
        } 

        // Phương thức xử lý nút "Remove"
        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            var cartLine = Cart.Lines.FirstOrDefault(cl => cl.Product.ProductID == productId);
            if (cartLine != null)
            {
                Cart.RemoveLine(cartLine.Product);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        // Phương thức xử lý nút "Increase"
        public IActionResult OnPostIncrease(long productId, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                Cart.AddItem(product, 1);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        // Phương thức xử lý nút "Decrease"
        public IActionResult OnPostDecrease(long productId, string returnUrl)
        {
            var productLine = Cart.Lines.FirstOrDefault(cl => cl.Product.ProductID == productId);
            if (productLine != null && productLine.Quantity > 1)
            {
                productLine.Quantity -= 1;
                Cart.UpdateItem(productLine.Product, productLine.Quantity);
            }
            else if (productLine != null && productLine.Quantity == 1)
            {
                Cart.RemoveLine(productLine.Product);
            }
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
