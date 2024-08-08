using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using shopMvc.Repo;
using ShopMvc.Core;
using ShopMvc.Core.ViewModels;
using ShopMvc.Repo.Data;
using System.Security.Claims;

namespace ShopMvc.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    { 

        private readonly IunitOfWork _unitOfWork;
 

        public CartVm Cart {  get; set; }  
        public CartController(IunitOfWork Unit)
        {
            _unitOfWork = Unit; 
            
        }
     
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            Cart = new CartVm()
            {
                Carts = _unitOfWork._RepoCart.GetAll(u => u.User_id == claims.Value, IncludeWords: "Products"),
            };

            foreach(  var item in Cart.Carts  )
            {
                Cart.Total_Price += (item.Products.ProductPrice * item.Count);
            }

            return View(Cart);
        }



        public  IActionResult increment(int CartId ) {

            var ShoppingCart = _unitOfWork._RepoCart.Get(u => u.ShoppingCartId == CartId);
          
            _unitOfWork._RepoCart.Inc_By1(ShoppingCart, 1);
                    
            _unitOfWork.Compelete();
            return RedirectToAction("Index");
        }


        public IActionResult decrement(int CartId)
        {

            var ShoppingCart = _unitOfWork._RepoCart.Get(u => u.ShoppingCartId == CartId);



            if (ShoppingCart.Count == 1) {

                _unitOfWork._RepoCart.Dec_By1(ShoppingCart, 1);
                _unitOfWork._RepoCart.Delete(ShoppingCart);
                _unitOfWork.Compelete();
                return Redirect("https://localhost:7094/");
            }
            else 
                {
                 _unitOfWork._RepoCart.Dec_By1(ShoppingCart, 1);
                _unitOfWork.Compelete();
                return RedirectToAction("Index");
                }


               
        }

        public IActionResult Delete(int CartId) {
            var shoppingCart = _unitOfWork._RepoCart.Get(u => u.ShoppingCartId == CartId);
            _unitOfWork._RepoCart.Delete(shoppingCart);
            _unitOfWork.Compelete();    
            return RedirectToAction("Index");
        } 


    }
}
