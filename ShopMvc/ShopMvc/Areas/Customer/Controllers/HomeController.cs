using Microsoft.AspNetCore.Mvc;
using shopMvc.Repo;
using ShopMvc.Core;
using ShopMvc.Core.Entities;
using ShopMvc.Core.ViewModels;

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
        public IActionResult Index()
        {
            var Products = _unit._ProductRepo.GetAll();
            return View(Products);
        }

        public IActionResult Description(int id) {

            if (id == null) { return NotFound(); }
            DescriptionProduct products = new DescriptionProduct()
            {
                Prod = _unit._ProductRepo.Get(p => p.Product_Id == id , IncludeWords:"Category"),
                Count = 1
            };
            return View(products);
        }  
    }
}
