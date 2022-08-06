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
            IEnumerable<CategoryModel> objCategoryList = _unitOfWork.Categories.GetAll();
            return View(objCategoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "TheDisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category successfully created";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Categories.GetFirstOrDefault(i => i.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "TheDisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Categories.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category successfully updated";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _unitOfWork.Categories.GetFirstOrDefault(i => i.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Categories.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Categories.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category successfully deleted";
            return RedirectToAction("Index");
        }
    }
}
