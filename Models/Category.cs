using System.ComponentModel.DataAnnotations;

namespace DigiShop.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [StringLength(150)]
        public string? CategoryName { get; set; }
        [StringLength(300)]
        public string? CategoryPhoto { get; set; }
        public string? CategoryOrder { get; set;}

        public string? CategoryQuantity { get; set; }
        
    }
}
