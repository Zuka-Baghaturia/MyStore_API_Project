using System.ComponentModel.DataAnnotations;

namespace WebApplication_MyStore_API_Project.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Url]
        public string ImageUrl { get; set; } = string.Empty;
    }
}

