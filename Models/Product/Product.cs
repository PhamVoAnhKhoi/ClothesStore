using System.ComponentModel.DataAnnotations.Schema;
namespace ClothesStore.Models {
    public class Product {
        public long? ProductID { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        public string? ProductImage {get; set;}
        public int QuantityOfProduct { get; set; }

        // Thêm thuộc tính CategoryID
        public long CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty; // Thêm thuộc tính CategoryName

        // Thêm thuộc tính Category
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }
    }
}