using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using shopMvc.Repo.Migrations;
using ShopMvc.Core;
using ShopMvc.Core.Entities;
using ShopMvc.Core.ViewModels;
using ShopMvc.Repo.Data;
using Stripe;

namespace ShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]

    
    public class OrderController : Controller
    {
        private readonly IunitOfWork _Unit;


        public OrderHeaderVm orderVm { get; set; }  

        public OrderController(IunitOfWork Unit)
        {
            _Unit = Unit;
        }
        public IActionResult Index()
        {
           return View();   
        }


        public IActionResult Getdata()
        {
            IEnumerable<OrderHeader> orders;
            orders = _Unit._RepoOrderHeader.GetAll(IncludeWords: "Users");
            return Json(new { data = orders });
        }

        public IActionResult Detail(int OrderHeaderId) {
            var orderVm = new OrderHeaderVm
            {
                orderHeader = _Unit._RepoOrderHeader.Get(u => u.OrderHeaderId == OrderHeaderId, IncludeWords: "Users"),
                orderDetails = _Unit._RepoOrderDetails.GetAll(u => u.OrderHeaderId == OrderHeaderId, IncludeWords: "Product")
            };

            return View(orderVm);   

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDetails(OrderHeaderVm ordervm) { 
            var order = _Unit._RepoOrderHeader.Get(u => u.OrderHeaderId == ordervm.orderHeader.OrderHeaderId , IncludeWords:"Users");

              
       
            if(ordervm.orderHeader.Carrier != null)
            {
                order.Carrier = ordervm.orderHeader.Carrier;
            }

             if(ordervm.orderHeader.Carrier != null)
            {
                order.TrackingNumber = ordervm.orderHeader.TrackingNumber;
            }

             _Unit._RepoOrderHeader.Update(order);
            _Unit.Compelete();
            TempData["Updated"] = "item Has Update Successfully";
            return RedirectToAction("Index","order",new { OrderHeaderId = ordervm.orderHeader.OrderHeaderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Start_Processing(OrderHeaderVm ordervm) {
            var order = _Unit._RepoOrderHeader.Get(u => u.OrderHeaderId == ordervm.orderHeader.OrderHeaderId);
            order.OrderStatus = ShopRoles.RolesSh.Processing;
            _Unit._RepoOrderHeader.Update(order);   
            _Unit.Compelete();
            TempData["Updated"] = "orderStatus Has Update Successfully";
            return RedirectToAction("Detail", "order", new { OrderHeaderId = ordervm.orderHeader.OrderHeaderId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Start_Shipping(OrderHeaderVm ordervm)
        {
            var order = _Unit._RepoOrderHeader.Get(u => u.OrderHeaderId == ordervm.orderHeader.OrderHeaderId);
            order.Carrier = ordervm.orderHeader.Carrier;    
            order.TrackingNumber = ordervm.orderHeader.TrackingNumber;
            order.ShippingDate = DateTime.UtcNow;
            order.OrderStatus = ShopRoles.RolesSh.Shipped;

            _Unit._RepoOrderHeader.Update(order);
            _Unit.Compelete();
            TempData["Updated"] = "orderStatus Has Update Successfully";
            return RedirectToAction("Detail", "order", new { OrderHeaderId = ordervm.orderHeader.OrderHeaderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CancelOrder(OrderHeaderVm ordervm)
        {
            var order = _Unit._RepoOrderHeader.Get(u => u.OrderHeaderId == ordervm.orderHeader.OrderHeaderId);
            if(order.PayementStatus == ShopRoles.RolesSh.Approve)
            {
                var option = new RefundCreateOptions { 
                Reason = RefundReasons.RequestedByCustomer,
                PaymentIntent = order.PayementId
                };
                var service = new RefundService();
                Refund refund = service.Create(option);
                _Unit._RepoOrderHeader.UpdateOrderStatues(order.OrderHeaderId, ShopRoles.RolesSh.Cancelled, ShopRoles.RolesSh.Refund);
            }
            else
            {
                _Unit._RepoOrderHeader.UpdateOrderStatues(order.OrderHeaderId, ShopRoles.RolesSh.Cancelled , ShopRoles.RolesSh.Cancelled);
            }

            _Unit.Compelete();
            TempData["Updated"] = "OrderStatus Has Update Successfully";
            return RedirectToAction("Index", "order", new { OrderHeaderId = ordervm.orderHeader.OrderHeaderId });
        }

    }
}
