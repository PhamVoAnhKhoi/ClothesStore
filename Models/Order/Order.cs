using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace ClothesStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();
        [Required(ErrorMessage = "Please enter a name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please enter the first address line")]
        //Số nhà 
        public string? Address { get; set; }
        //Tên đường
        public string? Street { get; set; }
        //Phường, xã
        public string? Block { get; set; }
        [Required(ErrorMessage = "Please enter a city name")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Please enter a state name")]
        public string? State { get; set; }
        public string? Zip { get; set; }
        [Required(ErrorMessage = "Please enter a country name")]
        public string? Country { get; set; }
        public bool GiftWrap { get; set; }
        [BindNever] 
        public bool Shipped { get; set; }
    }
}
