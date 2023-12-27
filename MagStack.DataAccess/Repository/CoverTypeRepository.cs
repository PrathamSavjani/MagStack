using MagStack.DataAccess.Repository.IRepository;
using MagStack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace MagStack.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;
        public CoverTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //In the code you provided, it looks like you are defining a new class
        //called CoverTypeRepository that inherits from a base class.
        //The base class is not shown in your code snippet, but based on the
        //signature of the constructor, it is likely a class that takes an instance
        //of ApplicationDbContext as a parameter.
        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
