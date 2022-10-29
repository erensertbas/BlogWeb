using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.BL.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            this._db = db;
          

            AboutUs = new AboutUsRepository(_db);
            
        }
        public IAboutUsRepository AboutUs { get; set; }
        public IBlogRepository Blog { get; set; }
        public ICategoryRepository Category { get; set; }
        public IContactRepository Contact { get; set; }
        public IImageRepository Image { get; set; }
        public IRoleRepository Role { get; set; }
        public ISubscriberRepository Subscriber { get; set; }
        public IUserRepository User { get; set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
