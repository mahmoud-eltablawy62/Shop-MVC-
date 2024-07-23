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
    public class CategoryRepo : GenaricRepo<Category>, ICategoryRepo
    {
        private readonly ShopDbContext _Context;
        public CategoryRepo(ShopDbContext Context) : base(Context)
        {
                _Context = Context;
        }

        public void update(Category category)
        {
            var cat = _Context.Categories.FirstOrDefault( x => x.Category_Id  == category.Category_Id );
            if (cat != null) { 
            cat.CreatedAt = DateTime.Now; 
            cat.Catogory_Description = category.Catogory_Description;
                cat.Category_Name = category.Category_Name;
            }   
        }  
    }
}
