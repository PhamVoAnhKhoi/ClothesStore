using Microsoft.AspNetCore.Mvc;
using ClothesStore.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClothesStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IStoreRepository storeRepository;
        private Cart cart;

        public OrderController(IOrderRepository orderRepo, IStoreRepository storeRepo, Cart cartService)
        {
            orderRepository = orderRepo;
            storeRepository = storeRepo;
            cart = cartService;
        }

        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (!cart.Lines.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                // Kiểm tra số lượng sản phẩm trong kho
                foreach (var line in cart.Lines)
                {
                    var product = storeRepository.Products.AsNoTracking().FirstOrDefault(p => p.ProductID == line.Product.ProductID);
                    if (product == null || product.QuantityOfProduct < line.Quantity)
                    {
                        ModelState.AddModelError("", "Not enough stock for product: " + line.Product.Name);
                        return View(order);
                    }
                }

                // Cập nhật số lượng sản phẩm và lưu đơn hàng
                try
                {
                    foreach (var line in cart.Lines)
                    {
                        var product = storeRepository.Products.AsNoTracking().FirstOrDefault(p => p.ProductID == line.Product.ProductID);
                        if (product != null)
                        {
                            product.QuantityOfProduct -= line.Quantity;
                            storeRepository.UpdateProduct(product); // Cập nhật số lượng sản phẩm
                        }
                    }

                    order.Lines = cart.Lines.ToArray();
                    orderRepository.SaveOrder(order); // Lưu đơn hàng
                    cart.Clear();
                    return RedirectToPage("/Completed", new { orderId = order.OrderID });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(order);
        }
    }
}
