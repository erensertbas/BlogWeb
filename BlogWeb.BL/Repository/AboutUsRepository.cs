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
    public class AboutUsRepository : Repository<AboutUs>, IAboutUsRepository
    {
        private ApplicationDbContext db;
        public AboutUsRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
    }
}
