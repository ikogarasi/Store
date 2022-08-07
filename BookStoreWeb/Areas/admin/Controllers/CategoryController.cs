using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess.Data;
using BookStore.Models;
using BookStore.DataAccess.Repository.IRepository;

namespace BookStoreWeb.Areas.admin.Controllers
{
    [Area("admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {         
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            CategoryModel categoryModel = new CategoryModel();

            if (id == null || id == 0)
            {
                return View(categoryModel);
            }

            categoryModel = _unitOfWork.Categories.GetFirstOrDefault(c => c.Id == id);
            return View(categoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CategoryModel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Categories.Add(obj);
                    TempData["success"] = "Category successfully created";
                }
                else
                {
                    _unitOfWork.Categories.Update(obj);
                    TempData["success"] = "Category successfully updated";
                }

                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var categoriesFromDb = _unitOfWork.Categories.GetAll();
            return Json(new {data = categoriesFromDb});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Categories.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Categories.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Category successfully deleted" });
        }
        #endregion
    }

}
