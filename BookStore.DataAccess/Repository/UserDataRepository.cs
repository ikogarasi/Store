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
    public class UserDataRepository : Repository<UserDataModel>, IUserDataRepository
    {
        private readonly ApplicationDbContext _db;

        public UserDataRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
