// Category.cs
namespace ClothesStore.Models
{
    public class Category
    {
        public long? CategoryID { get; set; }
        public string? CategoryImage {get; set;}
        public string CategoryName { get; set; } = String.Empty;
        public string CategoryDescription { get; set; } = String.Empty;
        
        // Thêm danh sách sản phẩm
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
