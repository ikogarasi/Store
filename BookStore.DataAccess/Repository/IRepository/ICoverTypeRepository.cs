using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverTypeModel>
    {
        void Update(CoverTypeModel obj);

        bool ContainsName(CoverTypeModel obj);
    }
}
