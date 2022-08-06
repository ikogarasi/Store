using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverTypeModel>, ICoverTypeRepository
    {
        private readonly ApplicationDbContext _db;

        
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CoverTypeModel obj)
        {
            _db.Update(obj);
        }

        public bool ContainsName(CoverTypeModel obj)
        {
            return (_db.CoverTypes.Any(i => i.Name == obj.Name));
        }
    }
}
