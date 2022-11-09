using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.BL.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IAboutUsRepository AboutUs { get; }
        IBlogRepository Blog { get; }
        IContactRepository Contact { get; }
        IImageRepository Image { get; }
        IRoleRepository Role { get; }
        ISubscriberRepository Subscriber { get; }
        IUserRepository User { get; }

        void Save();

    }
}
