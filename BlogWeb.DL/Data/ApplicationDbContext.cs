using BlogWeb.DL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Subscriber> Subscriber { get; set; }
        public DbSet<User> User { get; set; }


    }
}
