using Microsoft.AspNetCore.Mvc;
using ShopMvc.Core;
using ShopMvc.Core.Entities;


namespace ShopMvc.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IunitOfWork _Unit;

        public CategoryController(IunitOfWork Unit)
        {
            _Unit = Unit;
        }
        public IActionResult Index()
        {
            var Categories = _Unit._Repo.GetAll();
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                _Unit._Repo.Add(cat);
                _Unit.Compelete();
                TempData["Created"] = "Data Created Successfully";
                return RedirectToAction("Index");
            }

            return View(cat);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                var cat = _Unit._Repo.Get(x => x.Category_Id == Id);
                return View(cat);
            }
            else return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {

            if (ModelState.IsValid)
            {
                _Unit._Repo.update(cat);
                _Unit.Compelete();
                TempData["Updated"] = "Data Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(cat);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            else
            {
                var cat = _Unit._Repo.Get(C => C.Category_Id == id);
                return View(cat);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            //C => C.Category_Id == id
            var cat = _Unit._Repo.Get(C => C.Category_Id == id);
            _Unit._Repo.Delete(cat);
            _Unit.Compelete();
            TempData["Delete"] = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }


    }
}
