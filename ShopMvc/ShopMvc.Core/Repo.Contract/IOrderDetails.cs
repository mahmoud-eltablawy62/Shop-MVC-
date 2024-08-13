using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Repo.Contract
{
    public interface IOrderDetails : IGenaricRepo<OrderDetails> 
    {
        void Update(OrderDetails orderDetails);
    }
}
