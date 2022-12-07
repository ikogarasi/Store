using BookStore.Models;
using BookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartVM>
    {
        int IncrementQuantity(ShoppingCartVM shoppingCartVM, int count);
        int DecrementQuantity(ShoppingCartVM shoppingCartVM, int count);
    }
}
