using Microsoft.AspNetCore.Mvc;
using ShopMvc.Core.Entities;
using ShopMvc.Core;
using ShopMvc.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopMvc.Repo.Data;

namespace ShopMvc.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IunitOfWork _Unit;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ShopDbContext _context;

        public ProductController(IunitOfWork Unit, IWebHostEnvironment webHostEnvironment, ShopDbContext context)
        {

            _Unit = Unit;
            _webHostEnvironment = webHostEnvironment; 
            _context = context;
        }
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Getdata()
        {
            
            
            var  products = _Unit._ProductRepo.GetAll(IncludeWords : "Category").Select(p => new 
              {
                 
                 productName = p.ProductName,
                 productDescription = p.ProductDescription ,
                 productPrice = p.ProductPrice ,
                 category = _context.Categories.Where(c => c.Category_Id == p.CategoryId ).Select(c=>c.Category_Name),
                 ProductId = p.Product_Id,
            }); 

            return Json(new {data = products});
        }

        [HttpGet]
        public IActionResult Create()
        {

            ProductView productView = new ProductView()
            {
                Prod = new Product(),
                SelectListItems = _Unit._Repo.GetAll().Select( x => new SelectListItem
                {
                    Text = x.Category_Name ,
                    Value = x.Category_Id.ToString(),
                }),
            };
            return View(productView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductView Prods , IFormFile Upload)
        {

            if (ModelState.IsValid)
            {
                string bath = _webHostEnvironment.WebRootPath;
                if(Upload != null)
                {
                   string FileName = Guid.NewGuid().ToString();
                    var up = Path.Combine(bath, @"Images\Products\");
                    var ext = Path.GetExtension(Upload.FileName);
                    using (var filestream = new FileStream(Path.Combine(up, FileName + ext), FileMode.Create))
                    {
                        Upload.CopyTo(filestream);    
                    }
                    Prods.Prod.ProductImage = @"Images\Products\" + FileName + ext;
                }
                _Unit._ProductRepo.Add(Prods.Prod);
                _Unit.Compelete();
                TempData["Created"] = "Data Created Successfully";
                return RedirectToAction("Index");
            }

            return View(Prods);
        }

        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if(Id == null) { NotFound(); }
            ProductView productView = new ProductView()
            {
                Prod = _Unit._ProductRepo.Get(x => x.Product_Id == Id),
                SelectListItems = _Unit._Repo.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Category_Name,
                    Value = x.Category_Id.ToString(),
                }),
            };
            return View(productView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductView Prods , IFormFile ? Upload)
        {

            if (ModelState.IsValid)
            {
                string bath = _webHostEnvironment.WebRootPath;
                if (Upload != null)
                {
                    string FileName = Guid.NewGuid().ToString();
                    var up = Path.Combine(bath, @"Images\Products\");
                    var ext = Path.GetExtension(Upload.FileName);

                    if(Prods.Prod.ProductImage != null)
                    {
                        var Olding = Path.Combine(bath , Prods.Prod.ProductImage.TrimStart('\\')); 
                        if( System.IO.File.Exists(Olding) )
                        {
                            System.IO.File.Delete(Olding);
                        }
                    }


                    using (var filestream = new FileStream(Path.Combine(up, FileName + ext), FileMode.Create))
                    {
                        Upload.CopyTo(filestream);
                    }
                    Prods.Prod.ProductImage = @"Images\Products\" + FileName + ext;
                }
                _Unit._ProductRepo.update(Prods.Prod);
                _Unit.Compelete();
                TempData["updated"] = "Data Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(Prods.Prod);
        }

       

        [HttpDelete]
        
        public IActionResult Delete(int id)
        {
           
            var prod = _Unit._ProductRepo.Get(C => C.Product_Id == id);

            if (prod == null) { return Json(new { success = false, message = "error in Deleting" }); }
            string bath = _webHostEnvironment.WebRootPath;
            if (prod.ProductImage != null)
            {
                var Olding = Path.Combine(bath,prod.ProductImage.TrimStart('\\'));
                if (System.IO.File.Exists(Olding))
                {
                    System.IO.File.Delete(Olding);
                }
            }

            _Unit._ProductRepo.Delete(prod);
            _Unit.Compelete();
         
            return Json(new { success = true, message = " Item Deleting" });
        }
    }
}
