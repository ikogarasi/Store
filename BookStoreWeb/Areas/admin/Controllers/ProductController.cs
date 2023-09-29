using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging.Signing;

namespace BookStoreWeb.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = SD.Role_Admin)]
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
        public IActionResult Upsert(ProductVM obj, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Products.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Products.Update(obj.Product);
                }

                _unitOfWork.Save();

                string wwwRootPath = _hostEnvironment.WebRootPath;
                
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var productPath = @"images\product\product-" + obj.Product.Id;
                        var finalPath = Path.Combine(wwwRootPath, fileName);
                        
                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }

                        using (var fs = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fs);
                        }

                        ProductImage productImage = new()
                        {
                            ImageUrl = @$"\{productPath}\{fileName}",
                            ProductId = obj.Product.Id
                        };

                        if (obj.Product.ProductImages == null)
                        {
                            obj.Product.ProductImages = new List<ProductImage>();
                        }

                        obj.Product.ProductImages.Add(productImage);
                    }

                    _unitOfWork.Products.Update(obj.Product);
                    _unitOfWork.Save();
                    TempData["success"] = "Product successfully updated";
                }

                obj.Product.Description = obj.Product.Description.Replace("<p>", string.Empty)
                    .Replace("</p>", string.Empty);

                obj.Product.Price50 = obj.Product.Price / 100 * 90;
                obj.Product.Price100 = obj.Product.Price / 100 * 80;

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string? _ = null)
        {
            var productList = _unitOfWork.Products.GetAll(includeProperties:"CategoryModel,CoverTypeModel,ProductImages");
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

            //var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}

            _unitOfWork.Products.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Product successfully deleted" });
        }
        #endregion
    }

}
