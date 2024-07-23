using Microsoft.EntityFrameworkCore;
using ShopMvc.Models;
namespace ShopMvc
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> option) : base(option)
        {         
        }

        public DbSet<Category> Categories { get; set; }
    }
}
