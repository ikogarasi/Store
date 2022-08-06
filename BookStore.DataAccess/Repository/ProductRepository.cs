using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductModel obj)
        {
            var productFromDb = _db.Products.FirstOrDefault(i => i.Id == obj.Id);
            if (productFromDb != null)
            {
                foreach (PropertyInfo prop in typeof(ProductModel).GetProperties())
                {
                    if (prop.GetValue(obj) != null)
                    {
                        prop.SetValue(productFromDb, prop.GetValue(obj, null));
                    }
                }
            }
        }
    }
}
