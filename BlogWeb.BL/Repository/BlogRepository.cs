using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Data;
using BlogWeb.DL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.BL.Repository
{
    public class BlogRepository : GenericRepository<Blog>
    {
        //private ApplicationDbContext _db;
        //public BlogRepository(ApplicationDbContext db) : base(db)
        //{
        //    this._db = db;
        //}

    }
}
