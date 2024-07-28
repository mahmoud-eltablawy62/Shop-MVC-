using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMvc.Repo.Data;
using System.Drawing;
using System.Security.Claims;
using ShopRoles;

namespace ShopMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminsController : Controller
    {
        private readonly ShopDbContext _shopDbContext;
        public AdminsController(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
        public IActionResult Index()
        {
            ///Admin/Admins/index
            ///https://localhost:7094/Identity/Account/Login
            var claimsiden = (ClaimsIdentity)User.Identity;
            var claims = claimsiden.FindFirst(ClaimTypes.NameIdentifier);
            string userid = claims.Value;
            return View(_shopDbContext.GetUsers.Where(x => x.Id != userid).ToList());
        }

        public IActionResult Lock_UnLock(string id)
        {
            var user = _shopDbContext.GetUsers.FirstOrDefault(x => x.Id == id);
            if (user == null) { return NotFound(); }
            if (user.LockoutEnd == null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1);
            }
            else
            {
                user.LockoutEnd = null;
            }
            _shopDbContext.SaveChanges();
            return RedirectToAction("Index", "Admins", new { area = "Admin" });
        }
    }
}
