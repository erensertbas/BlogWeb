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
    public class RoleRepository: Repository<Role>, IRoleRepository
    {
        private ApplicationDbContext _db;
        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
