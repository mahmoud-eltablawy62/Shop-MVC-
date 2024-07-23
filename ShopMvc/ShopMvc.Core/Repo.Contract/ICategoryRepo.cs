using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Repo.Contract
{
    public interface ICategoryRepo : IGenaricRepo<Category>
    {
        void update(Category category);
    }
}
