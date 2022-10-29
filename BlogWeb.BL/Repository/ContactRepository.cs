using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Data;
using BlogWeb.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.BL.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private ApplicationDbContext _db;
        public ContactRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
    }
}
