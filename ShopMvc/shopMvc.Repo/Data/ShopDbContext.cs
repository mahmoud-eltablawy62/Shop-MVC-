using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ShopMvc.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopMvc.Repo.Data
{
    public class ShopDbContext : IdentityDbContext<IdentityUser>
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> option) : base(option)
        {         

        }

       

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get;set; }

        public DbSet<Users> GetUsers {  get; set; } 

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }  

        public DbSet<OrderHeader> OrderHeaders { get; set; }    


        public DbSet <OrderDetails> OrderDetails { get; set; }  


    }
}
