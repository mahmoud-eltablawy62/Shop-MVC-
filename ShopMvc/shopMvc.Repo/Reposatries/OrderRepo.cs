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
    public class OrderRepo : GenaricRepo<OrderHeader> , IOrderHeader
    {
        private readonly ShopDbContext  _DbContext;

        public OrderRepo(ShopDbContext DbContext) : base(DbContext)
        {
            _DbContext = DbContext; 
        }

        public void Update(OrderHeader header)
        {
            _DbContext.OrderHeaders.Update(header);
        } 

        public void  UpdateOrderStatues(int id, string Payment_Stat, string OrderStat)
        {
            var orderHeader = _DbContext.OrderHeaders.FirstOrDefault( u => u.OrderHeaderId == id);
            orderHeader.PaymentDate = DateTime.UtcNow;
            if (orderHeader != null) { 
            orderHeader.OrderStatus = OrderStat; 
            orderHeader.PayementStatus = OrderStat; 
            } 
        }
    }
} 
