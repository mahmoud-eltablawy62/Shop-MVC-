using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.ViewModels
{
     public class OrderHeaderVm
    {
        public OrderHeader orderHeader { get; set; }

        public IEnumerable<OrderDetails> orderDetails { get; set; } 


    }
}
