using ShopMvc.Core.Repo.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core
{
    public interface IunitOfWork : IDisposable
    {
        ICategoryRepo _Repo {  get; }

        IProductRepo _ProductRepo { get; }   
        int Compelete();    
    }
}
