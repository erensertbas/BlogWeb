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
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        private ApplicationDbContext db;
        public ImageRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
