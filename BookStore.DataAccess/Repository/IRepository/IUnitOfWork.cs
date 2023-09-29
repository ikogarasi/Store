using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ICoverTypeRepository CoverTypes { get; }
        IProductRepository Products { get; }
        ICompanyRepository Companies { get; }
        IUserDataRepository UsersData { get; }
        IShoppingCartRepository ShoppingCarts { get; }
		IOrderDetailRepository OrderDetails { get; }
		IOrderHeaderRepository OrderHeader { get; }
        IProductImageRepository ProductImages { get; }

		void Save();
    }
}
