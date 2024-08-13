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

        public IshopingCart _RepoCart { get; private set; } 

        public IProductRepo _ProductRepo { get; private set; }  

        public IOrderHeader _RepoOrderHeader { get; private set; }  

        public IOrderDetails _RepoOrderDetails { get; private set; }     

        public IAppUser _appUser { get; private set; }  
      
        public UnitOfWork(ShopDbContext Context) 
        {
            _Context = Context;
            _Repo = new CategoryRepo(Context);   
            _ProductRepo = new ProductRepo(Context);  
            _RepoCart = new ShopingCartRepo(Context);
            _RepoOrderHeader = new OrderRepo(Context); 
            _RepoOrderDetails = new OrderDetailsRepo(Context); 
            _appUser = new AppUser(Context);    
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
