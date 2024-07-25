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
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key] public int Product_Id { get; set; }

        [MaxLength(50)] [Required] public string ProductName { get; set; }

        [MaxLength(100)] public string ProductDescription { get; set; }

        [ValidateNever] public string? ProductImage { get; set; }

        [Required] public decimal  ProductPrice { get; set; }  


        [ForeignKey("Category")] public int  CategoryId { get; set; }

        [ValidateNever]public Category Category { get; set; }

    }
}
