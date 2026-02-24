using System.ComponentModel.DataAnnotations;

namespace WebApplication_MyStore_API_Project.DTO
{
    public class ProductDTO
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(200)]
        public string Category { get; set; } = string.Empty;
        [Required]
        [Range(0, 5)]
        public float Rating { get; set; }
        [Required]
        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
    }
}


