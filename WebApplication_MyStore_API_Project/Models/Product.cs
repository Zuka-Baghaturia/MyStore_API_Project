namespace WebApplication_MyStore_API_Project.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public float Rating { get; set; }
        public decimal Price { get; set; }


        public string ImageUrl { get; set; } = string.Empty;
    }
}



