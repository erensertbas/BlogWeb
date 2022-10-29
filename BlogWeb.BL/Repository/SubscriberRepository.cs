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
    internal class SubscriberRepository : Repository<Subscriber>, ISubscriberRepository
    {
        private ApplicationDbContext db;
        public SubscriberRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }
    }
}
