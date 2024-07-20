using Microsoft.EntityFrameworkCore;

namespace ClothesStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDBContext context;

        public EFOrderRepository(StoreDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            // Tách thực thể sản phẩm trước khi thêm hoặc cập nhật đơn hàng
            foreach (var line in order.Lines)
            {
                var trackedProduct = context.Products.Local.FirstOrDefault(p => p.ProductID == line.Product.ProductID);
                if (trackedProduct != null)
                {
                    context.Entry(trackedProduct).State = EntityState.Detached;
                }
                context.Attach(line.Product);
            }

            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }

            // Update product quantities
            foreach (var line in order.Lines)
            {
                var product = context.Products.FirstOrDefault(p => p.ProductID == line.Product.ProductID);
                if (product != null)
                {
                    product.QuantityOfProduct -= line.Quantity;
                    context.Entry(product).State = EntityState.Modified;
                }
            }

            context.SaveChanges();
        }
    }
}
