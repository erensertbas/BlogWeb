using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWeb.DL.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=cutopya.com\\MSSQLSERVER2017; Database=cutopyac_database;  Integrated Security=false; User Id=cutopyac_admin; Password=f%epX4186;");
        }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
