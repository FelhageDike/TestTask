using Microsoft.EntityFrameworkCore;
using ProductApi.DbModels;
using ProductApi.Models;

namespace ProductApi.DbModel
{
    public class ProductDbContext : DbContext
    {
        public DbSet<ProductDTO> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<ProductApi.Models.Product> Product { get; set; }
    }
}
