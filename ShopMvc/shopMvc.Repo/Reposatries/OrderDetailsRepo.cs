using Microsoft.EntityFrameworkCore;
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
    public class OrderDetailsRepo : GenaricRepo<OrderDetails> , IOrderDetails
    {
        private readonly ShopDbContext _shopDbContext;

        public OrderDetailsRepo( ShopDbContext shopDbContext ) : base(shopDbContext)
        {
             _shopDbContext = shopDbContext; 
        }

        public void Update(OrderDetails details)
        {
            _shopDbContext.OrderDetails.Update(details);
        }
    }
}
