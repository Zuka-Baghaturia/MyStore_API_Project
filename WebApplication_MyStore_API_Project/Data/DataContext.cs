using Microsoft.EntityFrameworkCore;
using WebApplication_MyStore_API_Project.Models;

namespace WebApplication_MyStore_API_Project.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=ProductsDB;Trusted_Connection=True;");
        }
    }
}
