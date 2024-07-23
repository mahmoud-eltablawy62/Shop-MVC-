using Microsoft.EntityFrameworkCore;
using ShopMvc.Core.Repo.Contract;
using ShopMvc.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace shopMvc.Repo.Reposatries
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : class
    {
        private readonly ShopDbContext _Context;
        private DbSet<T> _dbSet;

        public GenaricRepo(ShopDbContext Context)
        {
            _Context = Context;
            _dbSet = _Context.Set<T>();
        }

        public void Add(T Enity) => 
            _dbSet.Add(Enity);
        

        public void Delete(T Enity)=> 
            _dbSet.Remove(Enity);
        

        public T Get(Expression<Func<T, bool>> ? Predicate = null, string? IncludeWords=null)
        {
            IQueryable<T> quary = _dbSet;
            if (Predicate != null)
            {
                quary = quary.Where(Predicate);
            }
            if (IncludeWords != null)
            {
                foreach (var word in IncludeWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary.Include(word);
                }
            }
            return quary.SingleOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> ? Predicate = null, string? IncludeWords = null)
        {
            IQueryable<T> quary = _dbSet;
            if (Predicate != null)
            {
                quary = quary.Where(Predicate);
            }
            if (IncludeWords != null)
            {
                foreach (var word in IncludeWords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    quary.Include(word);
                }
            }
            return quary.ToList();
        }

      
       

       
    }

}
