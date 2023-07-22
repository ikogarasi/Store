using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
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
            CompanyModel companyFromDb = new CompanyModel();
            if (id == null || id == 0)
                return View(companyFromDb);

            companyFromDb = _unitOfWork.Companies.GetFirstOrDefault(i => i.Id == id);
            return View(companyFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CompanyModel obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Companies.Add(obj);
                    TempData["success"] = "Company successfully added";
                }
                else
                {
                    _unitOfWork.Companies.Update(obj);
                    TempData["success"] = "Company successfully updated";
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
            IEnumerable<CompanyModel> companiesFromDb = _unitOfWork.Companies.GetAll();
            return Json(new { data = companiesFromDb });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var companyFromDb = _unitOfWork.Companies.GetFirstOrDefault(i => i.Id == id);
            
            if (companyFromDb == null)
            {
                return Json(new { message = "Error while deleting" });
            }

            _unitOfWork.Companies.Remove(companyFromDb);
            _unitOfWork.Save();
            //TempData["success"] = "Company successfully removed";
            return Json(new { success = true, message = "Compnany successfully removed" }); 
        }
        #endregion
    }

}
