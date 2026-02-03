namespace WebApplication_MyStore_API_Project.DTO
{
    //public class CartDTO
    //{
    //    public int Id { get; set; }
    //    public int ProductId { get; set; }
    //    public string Title { get; set; } = string.Empty;
    //    public decimal Price { get; set; }
    //    public int Quantity { get; set; }
    //    public string ImageUrl { get; set; } = string.Empty;
    //}

    public class CartDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
