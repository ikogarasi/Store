using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //var usersFromDb = _unitOfWork.UsersData.GetAll(includeProperties: "Company");
            var usersFromDb = _db.UsersData.Include(i => i.Company).ToList();
            
            foreach (var user in usersFromDb)
            {
                if (user.Company == null)
                {
                    user.Company = new()
                    {
                        Name = ""
                    };
                }
            }
            
            return Json(new { data = usersFromDb });
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var userFromDb = _unitOfWork.UsersData.GetFirstOrDefault(i => i.Id == id);
            
            if (userFromDb == null)
            {
                return Json(new { message = "Error while deleting" });
            }

            _unitOfWork.UsersData.Remove(userFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "User successfully removed" }); 
        }
        #endregion
    }

}
