using System.ComponentModel.DataAnnotations;

namespace WebApplication_MyStore_API_Project.DTO
{
    public class CartDTO
    {
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }

}
