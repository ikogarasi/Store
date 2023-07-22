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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementQuantity(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Quantity -= count;
            return shoppingCart.Quantity;
        }

        public int IncrementQuantity(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Quantity += count;
            return shoppingCart.Quantity;
        }
    }
}
