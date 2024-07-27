
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMvc.Core.Entities
{
    public class Users : IdentityUser
    {

        public string Name { get; set; } 
        public string Address { get; set; } 

        public string City { get; set; }
    }
}
