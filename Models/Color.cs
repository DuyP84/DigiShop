using System.ComponentModel.DataAnnotations;

namespace DigiShop.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        [StringLength(50)]
        public string? ColorName { get; set; }
    }
}
