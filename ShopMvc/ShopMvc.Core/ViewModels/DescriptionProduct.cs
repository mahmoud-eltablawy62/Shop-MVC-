using ShopMvc.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.ViewModels
{
   public class DescriptionProduct
    {
        public Product Prod { get; set; }
        [Range(1, 100,ErrorMessage ="Range Of Count From 1 To 100")]
        public int Count { get; set; }  
    }
}
