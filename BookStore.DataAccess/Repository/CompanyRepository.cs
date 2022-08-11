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
    public class CompanyRepository : Repository<CompanyModel>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CompanyModel obj)
        {
            var companyFromDb = _db.Companies.FirstOrDefault(i => i.Id == obj.Id);
            if (companyFromDb != null)
            {
                foreach (PropertyInfo prop in typeof(CompanyModel).GetProperties())
                {
                    if (prop.GetValue(obj) != null)
                    {
                        prop.SetValue(companyFromDb, prop.GetValue(obj, null));
                    }
                }
            }

        }
    }
}
