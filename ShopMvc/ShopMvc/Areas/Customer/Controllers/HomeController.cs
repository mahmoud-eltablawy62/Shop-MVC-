using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using shopMvc.Repo;
using ShopMvc.Core;
using ShopMvc.Core.Entities;

using System.Security.Claims;

namespace ShopMvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IunitOfWork _unit;
        public HomeController(IunitOfWork unit)
        {
            _unit = unit;  
        }

        public IActionResult Index( )
        {
            
            var Products = _unit._ProductRepo.GetAll();
            return View(Products);
        }

        [HttpGet]
        public IActionResult Description(int id) {

            if (id == null) { return NotFound(); }
            ShoppingCart products = new ShoppingCart()
            {
                ProductId = id,
                Products = _unit._ProductRepo.Get(p => p.Product_Id == id , IncludeWords:"Category"),
                Count = 1
            };

            return View(products);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Description( ShoppingCart cart )
        {

            var claimsiden = (ClaimsIdentity)User.Identity;
            
            var claims = claimsiden.FindFirst(ClaimTypes.NameIdentifier);

            if(claims.Value == null)
            {
                return Redirect("http://shopanddashboardforadmin.runasp.net/Identity/Account/Login");
            }

            cart.User_id = claims.Value;

            ShoppingCart obj = _unit._RepoCart.Get
                ( u => u.User_id == claims.Value && u.ProductId == cart.ProductId );

            if (obj == null) {

                _unit._RepoCart.Add(cart);
            }
            else
            {
               _unit._RepoCart.inc_dec(obj, cart.Count);      
            }

            _unit.Compelete();

            return RedirectToAction("Index");       
        }

    }
}
