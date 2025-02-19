using Microsoft.EntityFrameworkCore;

namespace ClothesStore.Models
{
    // Giúp IStoreRepository truy cập vào database
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDBContext context;

        public EFStoreRepository(StoreDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
        public IQueryable<Category> Categories => context.Categories; // Triển khai property Categories

        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            var trackedEntity = context.Products.Local.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (trackedEntity != null)
            {
                context.Entry(trackedEntity).State = EntityState.Detached;
            }
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public IQueryable<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Products;
            }
            return context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
        }
    }
}
