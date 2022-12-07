using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BookStore.Models.ViewModels;

namespace BookStore.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CoverTypeModel> CoverTypes { get; set; }
        public DbSet<ProductModel> Products { get; set; }  
        public DbSet<UserDataModel> UsersData { get; set; }
        public DbSet<CompanyModel> Companies { get; set; }  
        public DbSet<ShoppingCartVM> ShoppingCarts { get; set; }
    }
}
