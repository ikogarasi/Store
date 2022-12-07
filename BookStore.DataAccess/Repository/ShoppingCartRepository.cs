using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCartVM>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementQuantity(ShoppingCartVM shoppingCartVM, int count)
        {
            shoppingCartVM.Quantity -= count;
            return shoppingCartVM.Quantity;
        }

        public int IncrementQuantity(ShoppingCartVM shoppingCartVM, int count)
        {
            shoppingCartVM.Quantity += count;
            return shoppingCartVM.Quantity;
        }
    }
}
