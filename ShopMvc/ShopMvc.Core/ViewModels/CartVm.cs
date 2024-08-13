using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.ViewModels
{
     public class CartVm
    {
        public IEnumerable<ShoppingCart> Carts { get; set; }

        public OrderHeader  OrderHeader { get; set; }

        public decimal Total_Price { get; set; }    
    }
}
