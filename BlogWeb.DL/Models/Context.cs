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
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Subscriber> Subscriber { get; set; }
        public DbSet<User> User { get; set; }


    }
}
