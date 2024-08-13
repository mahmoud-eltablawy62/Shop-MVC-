using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Entities
{
    public class OrderHeader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Key]    public int OrderHeaderId { get; set; } 

        public  string userId { get; set; }

        [ValidateNever]
        [ForeignKey("userId")] 
        public Users Users { get; set; } 

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime ShippingDate {  get; set; } = DateTime.UtcNow;

        public decimal TotalPrice { get; set; }  

        public string  OrderStatus { get; set; }    


        public string ? PayementStatus { get; set; } 

        public string ?  TrackingNumber { get; set; }   


        public string ? Carrier { get; set; }  

        public DateTime PaymentDate { get; set; } =DateTime.UtcNow;

        ///////////////////////// Stripe ///////////////////////////// 

        public string? SessionId { get; set; } 

        public string? PayementId { get; set; }

        /////////////////////// user data /////////////////////////////// 


        public string ?Name { get; set; }
        public string ? Address { get; set; }

        public string ?City { get; set; }

        public string ?phoneNumber { get; set; } 

        public string? Email { get; set; }   

    }
}
