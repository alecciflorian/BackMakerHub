using BackMakerHub.Models;
using Microsoft.EntityFrameworkCore;
namespace BackMakerHub.DbConnection
{
    public class DbLink : DbContext
    {
        public DbLink(DbContextOptions<DbLink> options) : base(options) 
        {
        }
        public DbSet<User> User { get; set; } = null!;
        public DbSet<CategoryClass> Categories { get; set; } = null!;
        public DbSet<Products> Products { get; set; }
        public DbSet<StockLog> StockLogs { get; set; } = null!; 
    }
}
