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

builder.Services.AddMvc(); //eklendi izinsiz y�nlendirme olmas�n diye
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
//});  // buras�

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
app.UseSession();  // buras�
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
     name: "BlogDetail",
     pattern: "MakaleDetay/{id}",
    defaults: new { controller = "Home", action = "BlogDetail", id = UrlParameter.Optional });


app.MapControllerRoute(
 name: "HomeCategory",
 pattern: "MakaleKategori/{id}",
defaults: new { controller = "Home", action = "HomeCategory", id = UrlParameter.Optional });



app.MapControllerRoute(
 name: "SignIn",
 pattern: "GirisYap",
defaults: new { controller = "Home", action = "SignIn", id = UrlParameter.Optional });

app.MapControllerRoute(
 name: "Registration",
 pattern: "KayitOl",
defaults: new { controller = "Home", action = "Registration", id = UrlParameter.Optional });

app.MapControllerRoute(
 name: "ForgotMyPassword",
 pattern: "SifremiUnuttum",
defaults: new { controller = "Home", action = "ForgotMyPassword", id = UrlParameter.Optional });


app.MapControllerRoute(
 name: "LogOut",
 pattern: "CikisYap",
defaults: new { controller = "Home", action = "LogOut", id = UrlParameter.Optional });


#endregion

#region AdminController
app.MapControllerRoute(
    name: "Index",
    pattern: "Yonetici",
   defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AllBlogs",
    pattern: "Makaleler",
   defaults: new { controller = "Admin", action = "AllBlogs", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Blogs",
    pattern: "Makalelerim",
   defaults: new { controller = "Admin", action = "Blogs", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogCreate",
    pattern: "MakaleOlustur",
   defaults: new { controller = "Admin", action = "BlogCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "ApproveBlogList",
    pattern: "MakaleOnayListesi",
   defaults: new { controller = "Admin", action = "ApproveBlogList", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUs",
    pattern: "YoneticiHakkimizda",
   defaults: new { controller = "Admin", action = "AboutUs", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUsEdit",
    pattern: "HakkimizdaDuzenle",
   defaults: new { contrtoller = "Admin", action = "AboutUsEdit", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUsCreate",
    pattern: "HakkimizdaOlustur",
   defaults: new { controller = "Admin", action = "AboutUsCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Category",
    pattern: "YoneticiKategori",
   defaults: new { controller = "Admin", action = "Category", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "CategoryCreate",
    pattern: "KategoriOlustur",
   defaults: new { controller = "Admin", action = "CategoryCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Contact",
    pattern: "YoneticiIletisim",
   defaults: new { controller = "Admin", action = "Contact", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "ContactCreate",
    pattern: "IletisimOlustur",
   defaults: new { controller = "Admin", action = "ContactCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Subscriber",
    pattern: "AboneListesi",
   defaults: new { controller = "Admin", action = "Subscriber", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "SubscriberCreate",
    pattern: "AboneEkle",
   defaults: new { controller = "Admin", action = "SubscriberCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Profil",
    pattern: "Profilim/{id}",
   defaults: new { controller = "Admin", action = "Profil" });

app.MapControllerRoute(
    name: "BlogDetail",
    pattern: "AdminMakaleDetay/{id}",
   defaults: new { controller = "Admin", action = "BlogDetail" });

app.MapControllerRoute(
    name: "GetBlog",
    pattern: "MakaleDuzenle/{id}",
   defaults: new { controller = "Admin", action = "GetBlog", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogDelete",
    pattern: "MakaleSil/{id}",
   defaults: new { controller = "Admin", action = "BlogDelete", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "ApproveBlog",
    pattern: "MakaleOnay/{id}",
   defaults: new { controller = "Admin", action = "ApproveBlog", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "GetAbout",
    pattern: "HakkimizdaDuzenle/{id}",
   defaults: new { controller = "Admin", action = "GetAbout", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "AboutUsDelete",
    pattern: "HakkimizdaSil/{id}",
   defaults: new { controller = "Admin", action = "AboutUsDelete", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "CategoryDelete",
    pattern: "KategoriSil/{id}",
   defaults: new { controller = "Admin", action = "CategoryDelete", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "GetCategory",
    pattern: "KategoriDuzenle/{id}",
   defaults: new { controller = "Admin", action = "GetCategory", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "ContactDelete",
    pattern: "IletisimSil/{id}",
   defaults: new { controller = "Admin", action = "ContactDelete", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "GetContact",
    pattern: "IletisimDuzenle/{id}",
   defaults: new { controller = "Admin", action = "GetContact", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "GetSubscriber",
    pattern: "AboneDuzenle/{id}",
   defaults: new { controller = "Admin", action = "GetSubscriber", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "SubscriberDelete",
    pattern: "AboneSil/{id}",
   defaults: new { controller = "Admin", action = "SubscriberDelete", id = UrlParameter.Optional });

#endregion

#region UserController
app.MapControllerRoute(
    name: "Index",
    pattern: "Yazar",
   defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "Blogs",
    pattern: "YazarMakalelerim",
   defaults: new { controller = "User", action = "Blogs", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogCreate",
    pattern: "YazarMakaleOlustur",
   defaults: new { controller = "User", action = "BlogCreate", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogDetail",
    pattern: "YazarMakaleDetay/{id}",
   defaults: new { controller = "User", action = "BlogDetail" });

app.MapControllerRoute(
    name: "GetBlog",
    pattern: "YazarMakaleDuzenle/{id}",
   defaults: new { controller = "User", action = "GetBlog", id = UrlParameter.Optional });

app.MapControllerRoute(
    name: "BlogDelete",
    pattern: "YazarMakaleSil/{id}",
   defaults: new { controller = "User", action = "BlogDelete", id = UrlParameter.Optional });
app.MapControllerRoute(
    name: "Profil",
    pattern: "YazarProfilim/{id}",
   defaults: new { controller = "User", action = "Profil" });

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
