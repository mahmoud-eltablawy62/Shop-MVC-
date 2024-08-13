using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Repo.Contract
{
    public interface IOrderHeader : IGenaricRepo<OrderHeader>
    {
        void Update (OrderHeader orderHeader);

        void UpdateOrderStatues(int id, string Payment_Stat, string OrderStat);

    }
}
