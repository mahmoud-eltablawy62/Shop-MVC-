using ShopMvc.Core.Entities;
using ShopMvc.Core.Repo.Contract;
using ShopMvc.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopMvc.Repo.Reposatries
{
    public class ProductRepo : GenaricRepo<Product>, IProductRepo
    {
        private readonly ShopDbContext _Context;

        public ProductRepo(ShopDbContext Context) : base(Context)
        {
            _Context = Context;
        }


        public void update(Product Products)
        {
            var prod = _Context.Products.FirstOrDefault(x => x.Product_Id == Products.Product_Id);
            if (prod != null)
            {
                prod.ProductName = Products.ProductName; 
                prod.ProductDescription = Products.ProductDescription;  
                prod.ProductImage = Products.ProductImage;  
                prod.ProductPrice = Products.ProductPrice;
                prod.CategoryId = Products.CategoryId;
            }
        }


    }
}
