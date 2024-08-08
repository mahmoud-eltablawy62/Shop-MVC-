using ShopMvc.Core.Entities;
using ShopMvc.Core.Repo.Contract;
using ShopMvc.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShopMvc.Core.Entities;

namespace shopMvc.Repo.Reposatries
{
    public class ShopingCartRepo : GenaricRepo<ShoppingCart>, IshopingCart
    {
        private readonly ShopDbContext _Context;
        public ShopingCartRepo(ShopDbContext Context) : base(Context)
        {
            _Context = Context;
        }


        public int Inc_By1(ShoppingCart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }

        public int Dec_By1(ShoppingCart cart, int count)
        {
            cart.Count -= count;
            return cart.Count;
        }
        public int inc_dec(ShoppingCart cart, int count)
        {
            if (cart.Count > count)
            {
                cart.Count = count;
                return cart.Count;
            }
            else if (cart.Count < count)
            {
                cart.Count += count;
                return cart.Count;
            }
            else return cart.Count;  
            
        }
    }
}

