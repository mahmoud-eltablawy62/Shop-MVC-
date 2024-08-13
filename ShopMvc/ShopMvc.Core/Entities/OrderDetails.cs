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
    public  class OrderDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]    
        [Key]    public int OrderdetailsId  { get; set; } 

        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        public int productId { get; set; }

        [ForeignKey("productId")]
        [ValidateNever]
        public Product Product { get; set; }  

        public int Count { get; set; }    

        public decimal Price { get; set; }  


    }
}
