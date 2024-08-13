
using Microsoft.AspNetCore.Mvc;

using ShopMvc.Core;
using ShopMvc.Core.Entities;
using ShopMvc.Core.ViewModels;

using System.Security.Claims;

using Stripe.Checkout;


using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

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


        [HttpGet]
        public IActionResult SummaryUI()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var shop = new CartVm()
            {
                Carts = _unitOfWork._RepoCart.GetAll(U => U.User_id == claims.Value , IncludeWords: "Products"),
                OrderHeader = new(),
            };

            shop.OrderHeader.Users = _unitOfWork._appUser.Get(U => U.Id == claims.Value);
            shop.OrderHeader.Name = shop.OrderHeader.Users.Name;
            shop.OrderHeader.Address = shop.OrderHeader.Users.Address;
            shop.OrderHeader.City = shop.OrderHeader.Users.City;
            shop.OrderHeader.phoneNumber = shop.OrderHeader.phoneNumber;
            shop.OrderHeader.Email = shop.OrderHeader.Users.Email; 

            foreach ( var item in shop.Carts)
            {
                shop.OrderHeader.TotalPrice = (item.Count * item.Products.ProductPrice);
            }
            return View(shop);

        }


        [HttpPost]
        [AutoValidateAntiforgeryToken] 
        public IActionResult SummaryUI( CartVm  Vm)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Vm.OrderHeader = new OrderHeader();

            Vm.Carts = _unitOfWork._RepoCart.GetAll( u => u.User_id == claims.Value , IncludeWords: "Products");

            Vm.OrderHeader.OrderStatus = ShopRoles.RolesSh.Pending;
            Vm.OrderHeader.OrderStatus = ShopRoles.RolesSh.Pending;
            Vm.OrderHeader.userId = claims.Value;


            foreach( var item in Vm.Carts)
            {
                Vm.OrderHeader.TotalPrice += (item.Count * item.Products.ProductPrice);
            } 
            _unitOfWork._RepoOrderHeader.Add(Vm.OrderHeader);
            _unitOfWork.Compelete();

            foreach (var item in Vm.Carts)
            {
                OrderDetails details = new OrderDetails()
                {
                    Count = item.Count,
                    Price = item.Products.ProductPrice, 
                    productId = item.ProductId, 
                    OrderHeaderId = Vm.OrderHeader.OrderHeaderId, 
                };

                _unitOfWork._RepoOrderDetails.Add(details);
                _unitOfWork.Compelete();
            }
            var domain = "https://localhost:7094/";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),



                Mode = "payment",
                SuccessUrl = domain + $"Customer/Cart/ConfirmCode?id={Vm.OrderHeader.OrderHeaderId}",
                CancelUrl = domain+$"Customer/Cart/Index",
            };


            foreach (var item in Vm.Carts) {


                var LineItemsop = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Products.ProductPrice) * 100, 
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Products.ProductName,
                        },
                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(LineItemsop);    
            };

            var service = new SessionService();

            Session session = service.Create(options);

            Vm.OrderHeader.SessionId = session.Id;

            Vm.OrderHeader.PayementId = session.PaymentIntentId;

            _unitOfWork.Compelete();

            Response.Headers.Add("Location", session.Url);
           
            return new StatusCodeResult(303);

 
        }


        public  IActionResult ConfirmCode(int id)
        {
            OrderHeader order = _unitOfWork._RepoOrderHeader.Get(u => u.OrderHeaderId == id);
            var service = new SessionService();
            Session session = service.Get(order.SessionId);
            if(session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork._RepoOrderHeader.UpdateOrderStatues(id ,ShopRoles.RolesSh.Approve, ShopRoles.RolesSh.Approve);
                _unitOfWork.Compelete();
            }
            List <ShoppingCart> Carts = _unitOfWork._RepoCart.GetAll(u => u.User_id == order.userId).ToList();
            _unitOfWork._RepoCart.RemoveRange(Carts);
            _unitOfWork.Compelete();
            return View(id);
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
