namespace ClothesStore.Models 
{ 
    public interface IStoreRepository 
    { 
        IQueryable<Product> Products { get; } 
        IQueryable<Category> Categories { get; } // Thêm property Categories
        void SaveProduct(Product p); 
        void CreateProduct(Product p); 
        void DeleteProduct(Product p);
        IQueryable<Product> SearchProducts(string searchTerm); // Thêm phương thức tìm kiếm
    } 
}