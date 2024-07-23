using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Repo.Contract
{
    public  interface IGenaricRepo<T> where T : class
    {
        T Get(Expression<Func<T, bool>> ? Predicate = null, string? IncludeWords = null);  
        IEnumerable<T> GetAll(Expression<Func<T , bool>> ? Predicate = null , string ? IncludeWords = null);    
        void Delete(T Enity);
        void Add (T Enity);    
       



    }
}
