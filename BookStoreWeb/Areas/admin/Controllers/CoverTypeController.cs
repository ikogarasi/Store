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
            IEnumerable<CoverTypeModel> listOfObject = _unitOfWork.CoverTypes.GetAll();
            return View(listOfObject);
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

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var coverTypeFromDb = _unitOfWork.CoverTypes.GetFirstOrDefault(i => i.Id == id);

            if (coverTypeFromDb == null)
                return NotFound();

            return View(coverTypeFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverTypes.GetFirstOrDefault(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.CoverTypes.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Type successfully removed";
            return RedirectToAction("Index");
        }
    }
}
