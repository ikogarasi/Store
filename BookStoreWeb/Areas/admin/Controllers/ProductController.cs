using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Areas.admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Categories.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                CoverTypeList = _unitOfWork.CoverTypes.GetAll().Select(
                    i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    })
            };


            if (id == null || id == 0)
            {
                return View(productVM);
            }   
           
            productVM.Product = _unitOfWork.Products.GetFirstOrDefault(i => i.Id == id);
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fs = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Products.Add(obj.Product);
                    TempData["success"] = "Product successfully created";
                }
                else
                {
                    _unitOfWork.Products.Update(obj.Product);
                    TempData["success"] = "Product successfully updated";
                }

                obj.Product.Price50 = obj.Product.Price / 100 * 90;
                obj.Product.Price100 = obj.Product.Price / 100 * 80;

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Products.GetAll(includeProperties:"CategoryModel,CoverTypeModel");
            return Json(new {data = productList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Products.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Products.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product successfully deleted" });
        }
        #endregion
    }

}
