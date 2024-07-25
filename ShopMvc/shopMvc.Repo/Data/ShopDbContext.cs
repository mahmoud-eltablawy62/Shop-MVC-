using Microsoft.EntityFrameworkCore;
using ShopMvc.Core.Entities;

namespace ShopMvc.Repo.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> option) : base(option)
        {         
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get;set; }
    }
}
