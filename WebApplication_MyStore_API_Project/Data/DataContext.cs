using Microsoft.EntityFrameworkCore;
using WebApplication_MyStore_API_Project.Models;

namespace WebApplication_MyStore_API_Project.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }



        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


    }


}

