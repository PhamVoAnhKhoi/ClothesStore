using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ClothesStore.Models
{
    public class StoreDBContext: IdentityDbContext<ApplicationUser>
    {
        // Contructor of Store DB 
        public StoreDBContext(DbContextOptions<StoreDBContext> options)
        :base(options){}
        // Thuộc tính giúp ánh xạ đến Data của Product
        // Giúp thực hiện CRUD cho product
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>(); // Thêm đoạn này để có thể truy cập Orders
         public DbSet<Category> Categories { get; set; } // Thêm DbSet cho Category
    }
}