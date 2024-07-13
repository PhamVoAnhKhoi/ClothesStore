namespace ClothesStore.Models.ViewModels 
{ 
    public class ProductsListViewModel 
    { 
        public IEnumerable<Product> Products { get; set; } 
        = Enumerable.Empty<Product>(); 
        public PagingInfo PagingInfo { get; set; } = new();
        //Lọc theo Category
        public string? CurrentCategory { get; set; }
    } 
}
