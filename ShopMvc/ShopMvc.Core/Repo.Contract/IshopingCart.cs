using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Repo.Contract
{
    public interface IshopingCart : IGenaricRepo<ShoppingCart>
    {
        int inc_dec(ShoppingCart cart, int count);

        int Inc_By1(ShoppingCart cart, int count);
        int Dec_By1 (ShoppingCart cart, int count); 
    }
}

