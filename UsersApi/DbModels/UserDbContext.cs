using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using UsersApi.Models;

namespace UsersApi.DbModel
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
    }
}
