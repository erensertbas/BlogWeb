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
          
            // sorun burada a qqqqqqqqqqqq
            AboutUs = new AboutUsRepository(_db);
            Blog = new BlogRepository(_db);
            Category = new CategoryRepository(_db);
            Contact=new ContactRepository(_db);
            Image = new ImageRepository(_db);
            Role = new RoleRepository(_db);
            Subscriber = new SubscriberRepository(_db);
            User = new UserRepository(_db);
            
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
