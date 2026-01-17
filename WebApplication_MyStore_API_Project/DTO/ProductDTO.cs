namespace WebApplication_MyStore_API_Project.DTO
{
    public class ProductDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public float Rating { get; set; }
        public decimal Price { get; set; }


        public IFormFile? Image { get; set; }
    }
}
