using Microsoft.EntityFrameworkCore;
using SearchApi.Models;

namespace SearchApi.DbModel
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
    }
}
