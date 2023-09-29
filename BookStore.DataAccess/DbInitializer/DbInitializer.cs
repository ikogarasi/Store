using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        
        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public void Initialize()
        {
            try
            {
                
                _db.Database.EnsureCreated();
            }
            catch
            {

            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new UserDataModel
                {
                    UserName = "admin@admin.net",
                    Email = "admin@admin.net",
                    Name = "Admin",
                }, "Admin01_").GetAwaiter().GetResult();

                var user = _db.Users.FirstOrDefault(i => i.UserName == "admin@admin.net");

                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

                _unitOfWork.Categories.Add(new() { CreatedDateTime = DateTime.Now, DisplayOrder = 1, Name = "Fiction" });
                _unitOfWork.Categories.Add(new() { CreatedDateTime = DateTime.Now, DisplayOrder = 2, Name = "Non-Fiction" });
                _unitOfWork.Categories.Add(new() { CreatedDateTime = DateTime.Now, DisplayOrder = 3, Name = "Mystery" });
                _unitOfWork.Categories.Add(new() { CreatedDateTime = DateTime.Now, DisplayOrder = 4, Name = "Adventure" });

                _unitOfWork.CoverTypes.Add(new() { Name = "Hard Cover" });
                _unitOfWork.CoverTypes.Add(new() { Name = "Paper Cover" });

                _unitOfWork.Save();
            }
        }
    }
}
