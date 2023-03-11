using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));

});

builder.Services.AddMvc(); //eklendi izinsiz yönlendirme olmasýn diye
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.Cookie.Name = "NetCoreMvc.Auth";
    x.AccessDeniedPath = "/User/Index";
    x.LoginPath = "/Login/SignIn";
});

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromSeconds(10);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});  // burasý

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
app.MapDefaultControllerRoute();
app.UseRouting();
app.UseSession();  // burasý
app.UseAuthentication();// eklendi 


app.UseAuthorization();

#region HomeController

app.MapControllerRoute(
     name: "Anasayfa",
     pattern: "Anasayfa",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

app.MapControllerRoute(
     name: "Iletisim",
     pattern: "Iletisim",
    defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional });

app.MapControllerRoute(
     name: "Hakkimizda",
     pattern: "Hakkimizda",
    defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional });

app.MapControllerRoute(
     name: "MakaleDetay",
     pattern: "{Makale-Detay}/{id}",
    defaults: new { controller = "Home", action = "MakaleDetay", id = UrlParameter.Optional });


    app.MapControllerRoute(
     name: "HomeCategory",
     pattern: "{Makale-Kategori}/{id}",
    defaults: new { controller = "Home", action = "HomeCategory", id = UrlParameter.Optional });



#endregion

#region AdminController
//app.MapControllerRoute(
//    name: "Index",
//    pattern: "Yonetici",
//   defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "Profil",
//    pattern: "Profilim",
//   defaults: new { controller = "Admin", action = "Profil", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "Blogs",
//    pattern: "Makaleler",
//   defaults: new { controller = "Admin", action = "Blogs", id = UrlParameter.Optional });
//app.MapControllerRoute(
//    name: "BlogDetail",
//    pattern: "{MakaleDetay}/{id}",
//   defaults: new { controller = "Admin", action = "BlogDetail", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "BlogCreate",
//    pattern: "MakaleOlustur",
//   defaults: new { controller = "Admin", action = "BlogCreate", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "GetBlog",
//    pattern: "{MakaleDuzenle}/{id}",
//   defaults: new { controller = "Admin", action = "GetBlog", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "BlogDelete",
//    pattern: "{MakaleSil}/{id}",
//   defaults: new { controller = "Admin", action = "BlogDelete", id = UrlParameter.Optional });
//app.MapControllerRoute(
//    name: "ApproveBlogList",
//    pattern: "MakaleOnayListesi",
//   defaults: new { controller = "Admin", action = "ApproveBlogList", id = UrlParameter.Optional });
//app.MapControllerRoute(
//    name: "ApproveBlog",
//    pattern: "{MakaleOnay}/{id}",
//   defaults: new { controller = "Admin", action = "ApproveBlog", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "AboutUs",
//    pattern: "YoneticiHakkimizda",
//   defaults: new { controller = "Admin", action = "AboutUs", id = UrlParameter.Optional });
//app.MapControllerRoute(
//    name: "AboutUsEdit",
//    pattern: "HakkimizdaDuzenle",
//   defaults: new { contrtoller = "Admin", action = "AboutUsEdit", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "AboutUsCreate",
//    pattern: "HakkimizdaOlustur",
//   defaults: new { controller = "Admin", action = "AboutUsCreate", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "GetAbout",
//    pattern: "{HakkimizdaDuzenle}/{id}",
//   defaults: new { controller = "Admin", action = "GetAbout", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "AboutUsDelete",
//    pattern: "{HakkimizdaSil}/{id}",
//   defaults: new { controller = "Admin", action = "AboutUsDelete", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "Category",
//    pattern: "YoneticiKategori",
//   defaults: new { controller = "Admin", action = "Category", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "CategoryCreate",
//    pattern: "KategoriOlustur",
//   defaults: new { controller = "Admin", action = "CategoryCreate", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "CategoryDelete",
//    pattern: "{KategoriSi}l/{id}",
//   defaults: new { controller = "Admin", action = "CategoryDelete", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "GetCategory",
//    pattern: "{KategoriDuzenle}/{id}",
//   defaults: new { controller = "Admin", action = "GetCategory", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "Contact",
//    pattern: "YoneticiIletisim",
//   defaults: new { controller = "Admin", action = "Contact", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "ContactCreate",
//    pattern: "IletisimOlustur",
//   defaults: new { controller = "Admin", action = "ContactCreate", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "ContactDelete",
//    pattern: "{IletisimSi}l/{id}",
//   defaults: new { controller = "Admin", action = "ContactDelete", id = UrlParameter.Optional });

//app.MapControllerRoute(
//    name: "GetContact",
//    pattern: "{IletisimDuzenle}/{id}",
//   defaults: new { controller = "Admin", action = "GetContact", id = UrlParameter.Optional });

#endregion

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute(
     name: "Anasayfa",
     pattern: "Anasayfa", // url ismi
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Anasayfa}/{id?}"
    );


   // app.MapControllerRoute(
   //name: "default",
   //pattern: "{controller=Home}/{action=Anasayfa}/{id?}");

});

app.Run();
