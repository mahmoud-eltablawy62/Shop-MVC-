using Microsoft.AspNetCore.Mvc;
using ShopMvc.Core;
using ShopMvc.Core.Entities;
using ShopMvc.Core.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ShopMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IunitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger , IunitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
          return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
