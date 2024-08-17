using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMvc.Core;
using System.Security.Claims;

namespace ShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        private readonly IunitOfWork _unitOfWork;

        public DashBoardController(IunitOfWork UNit)
        {
            _unitOfWork = UNit;
        }
        public IActionResult Index()
        {
            var claimsiden = (ClaimsIdentity)User.Identity;
            var claims = claimsiden.FindFirst(ClaimTypes.NameIdentifier);
            ViewBag.Orders = _unitOfWork._RepoOrderHeader.GetAll().Count();
            ViewBag.ApprovedOrders = _unitOfWork._RepoOrderHeader.GetAll(u => u.OrderStatus == ShopRoles.RolesSh.Approve).Count();
            ViewBag.RegestrationUser = _unitOfWork._appUser.GetAll( u => u.Id != claims.Value ).Count();
            ViewBag.AllProduct = _unitOfWork._ProductRepo.GetAll().Count();
            return View();
        }
    }
}
