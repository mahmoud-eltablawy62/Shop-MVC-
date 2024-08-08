using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Entities
{
    public  class ShoppingCart 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int ShoppingCartId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]  public Product  Products { get; set; }

        public string User_id { get; set; }
        [ForeignKey("User_id")]
        [ValidateNever]  public  Users  User  { get; set; }

        public int Count { get; set; }
    }
}
