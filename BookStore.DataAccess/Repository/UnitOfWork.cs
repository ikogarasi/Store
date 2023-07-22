using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            CoverTypes = new CoverTypeRepository(_db);
            Products = new ProductRepository(_db);
            Companies = new CompanyRepository(_db);
            UsersData = new UserDataRepository(_db);
            ShoppingCarts = new ShoppingCartRepository(_db);
            OrderDetails = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
        }

        public ICategoryRepository Categories { get; }
        public ICoverTypeRepository CoverTypes { get; }
        public IProductRepository Products { get; }
        public ICompanyRepository Companies { get; }
        public IUserDataRepository UsersData { get; }
        public IShoppingCartRepository ShoppingCarts { get; }
        public IOrderDetailRepository OrderDetails { get; }
        public IOrderHeaderRepository OrderHeader { get; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
