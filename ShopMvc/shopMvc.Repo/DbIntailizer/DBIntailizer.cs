using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using ShopMvc.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopRoles;
using ShopMvc.Core.Entities;


namespace shopMvc.Repo.DbIntailizer
{
    public class DBIntailizer : IDBIntailizer
    {
        private readonly RoleManager<IdentityRole> _roleManager;    
        private readonly UserManager<IdentityUser> _userManager;    
        private readonly ShopDbContext _shopDbContext;
        public DBIntailizer(
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ShopDbContext shopDbContext
            )
        {
            _shopDbContext = shopDbContext;
            _roleManager = roleManager; 
            _userManager = userManager; 
        }
        public  void Intailizer() {
            //Migrations 
            try
            {
                if(_shopDbContext.Database.GetPendingMigrations().Count() > 0) 
                     _shopDbContext.Database.Migrate(); 
            }
            catch (Exception)
            {
                throw;
            }
            //Roles 
            if (!_roleManager.RoleExistsAsync(RolesSh.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(RolesSh.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RolesSh.EmpRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(RolesSh.CustomerRole)).GetAwaiter().GetResult();


                _userManager.CreateAsync(new Users()
                {
                    UserName = "MahmoudSaidEltablawy",
                    Email = "mahmoudeltablawy702@gmail.com",
                    Name = "MahmoudEltablawy",
                    PhoneNumber= "01009033369",
                    Address = "Tanta",
                    City ="Kafr_ElZayat",

                },"262002Aml_").GetAwaiter().GetResult(); 

                var user = _shopDbContext.Users.FirstOrDefault( u=>u.Email == "mahmoudeltablawy702@gmail.com"); 
                _userManager.AddToRoleAsync(user, RolesSh.AdminRole);   

                
            
            }

            return;
            
        }

    }
}
