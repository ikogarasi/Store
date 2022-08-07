using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.admin.Controllers
{
    [Area("admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverTypeModel obj)
        {
            if (_unitOfWork.CoverTypes.ContainsName(obj))
                ModelState.AddModelError("Name", "Type with the same name already exists");
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypes.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Type successfully created";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var listOfObject = _unitOfWork.CoverTypes.GetAll();
            return Json(new { data = listOfObject });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.CoverTypes.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.CoverTypes.Remove(obj);
            _unitOfWork.Save();
            return Json(new {success = true, message = "Cover type successfully deleted"});
        }
        #endregion
    }
}
