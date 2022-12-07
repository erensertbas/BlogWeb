using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region HomeController

app.MapControllerRoute(
     name: "Anasayfa",
     pattern: "Anasayfa",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

app.MapControllerRoute(
     name: "Contact",
     pattern: "Iletisim",
    defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional });

app.MapControllerRoute(
     name: "About",
     pattern: "Hakkimizda",
    defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional });

#endregion

#region AdminController
app.MapControllerRoute(
    name: "Index",
    pattern: "Yonetici",
   defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Blogs",
    pattern: "Makaleler",
   defaults: new { controller = "Admin", action = "Blogs", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogCreate",
    pattern: "MakaleOlustur",
   defaults: new { controller = "Admin", action = "BlogCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "GetBlog",
    pattern: "{MakaleDuzenle}/{id}",
   defaults: new { controller = "Admin", action = "GetBlog", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogDelete",
    pattern: "{MakaleSil}/{id}",
   defaults: new { controller = "Admin", action = "BlogDelete", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUs",
    pattern: "YoneticiHakkimizda",
   defaults: new { controller = "Admin", action = "AboutUs", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUsCreate",
    pattern: "HakkimizdaOlustur",
   defaults: new { controller = "Admin", action = "AboutUsCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "GetAbout",
    pattern: "{HakkimizdaDuzenle}/{id}",
   defaults: new { controller = "Admin", action = "GetAbout", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUsDelete",
    pattern: "{HakkimizdaSil}/{id}",
   defaults: new { controller = "Admin", action = "AboutUsDelete", id = UrlParameter.Optional });

#endregion



app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
     name: "default",
     pattern: "default",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

    // endpoints.MapControllerRoute(
    //   name: "areas",
    //   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    // );


    // app.MapControllerRoute(
    //name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
