using Microsoft.AspNetCore.Mvc;
using ShopMvc.Models;

namespace ShopMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShopDbContext _Context;

        public CategoryController(ShopDbContext Context)
        {
            _Context = Context;   
        }
        public IActionResult Index()
        {
            var Categories = _Context.Categories.ToList();
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken] /// ====> cross side Frogery Attack 
        public IActionResult Create(Category cat) 
        {
            if (ModelState.IsValid)
            {
                _Context.Categories.Add(cat);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Edit(int ? Id) {
            if (Id != null)
            {
                var cat = _Context.Categories.FirstOrDefault(c => c.Category_Id == Id);
                return View(cat);   
            }
            else return NotFound();
             }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat) {

            if (ModelState.IsValid)
            {
                _Context.Categories.Update(cat);
                _Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }

        [HttpGet]
        public IActionResult Delete(int? id) {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            else
            {
                var cat = _Context.Categories.FirstOrDefault(C => C.Category_Id == id);
                return View(cat);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            var cat = _Context.Categories.FirstOrDefault(C => C.Category_Id == id);
            _Context.Categories.Remove(cat);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }


    } 
}
