using shopMvc.Repo.Reposatries;
using ShopMvc.Core;
using ShopMvc.Core.Repo.Contract;
using ShopMvc.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopMvc.Repo
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly ShopDbContext _Context;
        public ICategoryRepo _Repo { get; private set; }

        public IProductRepo _ProductRepo { get; private set; }  
        public UnitOfWork(ShopDbContext Context) 
        {
            _Context = Context;
            _Repo = new CategoryRepo(Context);   
            _ProductRepo = new ProductRepo(Context);    
        }

        public int Compelete()
        {
           return _Context.SaveChanges();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }
    }
}
