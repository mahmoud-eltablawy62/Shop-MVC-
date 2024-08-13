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
    public class AppUser : GenaricRepo<Users> , IAppUser
    {
        private readonly ShopDbContext _Context;
        public AppUser(ShopDbContext Context) : base( Context)
        {
            _Context = Context;
        }
    }
}
