using System.ComponentModel.DataAnnotations;

namespace DigiShop.Models
{
    public class Size
    {
        [Key]
        public int SizeId { get; set; }
        [StringLength(50)]
        public string? SizeName { get; set; }
    }
}
