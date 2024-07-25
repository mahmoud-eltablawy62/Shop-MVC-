using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ShopMvc.Core.ViewModels
{
    public class ProductView
    {

        public Product Prod { get; set; }

        [ValidateNever] public IEnumerable<SelectListItem> SelectListItems { get; set; }    
    }
}
