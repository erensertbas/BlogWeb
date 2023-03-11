using BlogWeb.BL.Repository;
using BlogWeb.BL.Repository.IRepository;
using BlogWeb.DL.Models;
using BlogWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Text;
using MimeKit;
//using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Org.BouncyCastle.Crypto.Macs;
using Firebase.Auth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Web.WebPages;
using BlogWeb.PL.Models;

namespace BlogWeb.PL.Controllers
{

    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AboutUsRepository aboutUs = new AboutUsRepository();
        ContactRepository contact = new ContactRepository();
        CategoryRepository category = new CategoryRepository();
        BlogRepository blog = new BlogRepository();
        SubscriberRepository subscriber = new SubscriberRepository();
        Context c = new Context();


        FirebaseAuthProvider auth;
        UserRepository user = new UserRepository();

        //[Route("Anasayfa")]
        public IActionResult Index(int? pageNumber)
        {

            int pageSize = 5;

            var result = PaginatedList<Blog>.Create(c.Blog.Distinct().OrderByDescending(d => d.Date).Where(x => x.Status == true).ToList(), pageNumber ?? 1, pageSize);

            return View(result);
        }

        #region home
        public ActionResult HomeCategory(int id, int? pageNumber)
        {
            int pageSize = 2;

            var cat = category.TGet(id).CategoryName;
            TempData["Kategori"] = cat;

            var result = c.Blog.Where(x => x.CategoryId == id).ToList();

            if (result.Count() > 0)
            {

                var result1 = PaginatedList<Blog>.Create(c.Blog.Distinct().OrderByDescending(d => d.Date).Where(x => x.Status == true && x.CategoryId == id).ToList(), pageNumber ?? 1, pageSize);


                return View(result1);
            }

            return RedirectToAction("Index");
        }



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            auth = new FirebaseAuthProvider(
                           new FirebaseConfig("AIzaSyA0qXMD0sg1OMh-KYkL60vzauVTcLy60aA"));
        }


        public IActionResult SubscriberAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubscriberAdd(Subscriber sub)
        {
            if (ModelState.IsValid)
            {
                subscriber.TAdd(sub);
                TempData["EklemeSonuc"] = 1;
                return RedirectToAction("Index");
            }
            return View();
        }


        ////[Route("{MakaleDetay}/{id}")]
        //public IActionResult MakaleDetay(int id)
        //{
        //    var result = blog.TGet(id);
        //    var categoryName = category.TGet(result.CategoryId).CategoryName;
        //    var userName = user.TGet(result.UserId);
        //    ViewBag.userName = userName.FirstName + userName.LastName;
        //    ViewBag.categoryName = categoryName;
        //    return View(result);

        //}


        [HttpGet]
        //[Route("Iletisim")]
        public IActionResult Contact()
        {
            IEnumerable<Contact> _contact = contact.TList();

            ViewBag.Contact = _contact;
            return View();

        }
        [HttpPost]
        public IActionResult Contact(Message model)
        {

            //if (ModelState.IsValid)
            //{
            //    MimeMessage mimeMessage = new MimeMessage();

            //    MailboxAddress mailboxAddressFrom = new MailboxAddress("Destek", "cutopya@gmail.com");

            //    mimeMessage.From.Add(mailboxAddressFrom);

            //    MailboxAddress mailboxAddressTo = new MailboxAddress("Admin", "info@cutopya.com");
            //    mimeMessage.To.Add(mailboxAddressTo);

            //    var bodyBuilder = new BodyBuilder();
            //    bodyBuilder.TextBody = "Maili Gönderen : " + model.Firstname + " " + model.Lastname + "\n" + " Maili : " + model.Email + "\n" + model.Content;
            //    mimeMessage.Body = bodyBuilder.ToMessageBody();

            //    mimeMessage.Subject = model.Topic;

            //    SmtpClient client = new SmtpClient();
            //    client.Connect("smtp.gmail.com", 25, false);
            //    client.Authenticate("cutopya@gmail.com", "zstvndudhopkukec");
            //    client.Send(mimeMessage);
            //    client.Disconnect(true);
            //}
            return View();
        }

        //[Route("Hakkimizda")]

        public IActionResult About()
        {
            IEnumerable<AboutUs> aboutus = aboutUs.TList();
            return View(aboutus);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion


        #region Login Register LogOut

        [AllowAnonymous]

        public IActionResult SignIn()
        {
            return View();
        }


        public void SetCookie(string key, string id)
        {
            HttpContext.Response.Cookies.Append(key, id);
        }

        public string GetCookie(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string id);
            return id;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserModel model)
        {
            Context c = new Context();

            var dataValue = c.User.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (dataValue != null)
            {
                if (dataValue.RoleId == 1)
                {
                    var claims = new List<Claim> {

                        new Claim(ClaimTypes.Name,model.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
                    var userIdentity = new ClaimsIdentity(claims, "a");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    HttpContext.Session.SetInt32("_UserToken", dataValue.UserId);

                    SetCookie("userId", dataValue.UserId.ToString());

                    return RedirectToAction("Index", "Home"); // admin ındex
                }
                else
                {
                    var claims = new List<Claim> {

                        new Claim(ClaimTypes.Name,model.Email),
                        new Claim(ClaimTypes.Role, "User")
                    };
                    var userIdentity = new ClaimsIdentity(claims, "a");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    HttpContext.Session.SetInt32("_UserToken", dataValue.UserId);
                    SetCookie("userId", dataValue.UserId.ToString());
                    return RedirectToAction("Index", "Home"); // user ındex
                }

            }
            else
            {
                TempData["ErrorMessage"] = "Mail adresiniz veya şifreniz hatalı!";
            }
            return View();


        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserModel Model) // kayıt ol modeli
        {
            Model.RoleId = 2;
            try
            {
                var a = await auth.CreateUserWithEmailAndPasswordAsync(Model.Email, Model.Password, Model.FirstName, true);
                TempData["MailOnay"] = "Lütfen Mail Adresinizi Onaylayınız";
                user.TAdd(Model);
                return RedirectToAction("SignIn");
            }
            catch (Exception ex)
            {
                if (Response.StatusCode == 200)
                {
                    TempData["Hata"] = "E mail mevcut!";
                }
            }

            return View();

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ForgotMyPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgotMyPassword(UserModel model)
        {
            if (model.Email != null)
            {
                var email = user.TList().Where(x => x.Email == model.Email).Count();
                if (email == 0)
                {
                    TempData["Message"] = "Bu emaile ait kullanıcı bulunamadı";
                }
                else
                {
                    var kontrol = user.TList(X => X.Email == model.Email).FirstOrDefault();
                    if (kontrol.Email == model.Email)
                    {
                        Random rastgele = new Random();
                        string newPassword = null;

                        //İki büyük harf üretme
                        for (int i = 1; i <= 2; i++)
                        {
                            int sayi1 = rastgele.Next(65, 91);
                            newPassword = newPassword + ((char)sayi1);
                        }
                        //İki küçük harf üretme
                        for (int i = 1; i <= 2; i++)
                        {
                            int sayi1 = rastgele.Next(97, 123);
                            newPassword = newPassword + ((char)sayi1);
                        }
                        //İki sayı üretme
                        for (int i = 1; i <= 2; i++)
                        {
                            int sayi1 = rastgele.Next(48, 58);
                            newPassword = newPassword + ((char)sayi1);
                        }
                        //iki sembol üretme
                        for (int i = 1; i <= 2; i++)
                        {
                            int sayi1 = rastgele.Next(35, 39);
                            newPassword = newPassword + ((char)sayi1);
                        }

                        MailMessage mailMessage = new MailMessage();
                        mailMessage.To.Add(model.Email);
                        mailMessage.From = new MailAddress("cutopya@gmail.com");
                        mailMessage.Subject = "Şifre Yenileme Talebi Hakkında";
                        mailMessage.Body = "Sayın " + kontrol.FirstName + " " + kontrol.LastName + " Cütopya uygulaması üzerinden \n" + DateTime.Now.ToString() + " tarihinde göndermiş olduğunuz şifre değiştirme talebinize istinaden oluşturulan yeni şifreniz aşağıda belirtilmiştir \n " + "Yeni Şifreniz : " + newPassword + " Sisteme giriş yaptıktan sonra, lütfen şifrenizi değiştiriniz";
                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtp = new SmtpClient();
                        smtp.Credentials = new NetworkCredential("cutopya@gmail.com", "dkjvwhkwxanvpkwq");
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(mailMessage);

                        var x = user.TGet(kontrol.UserId);

                        x.Email = kontrol.Email;
                        x.FirstName = kontrol.FirstName;
                        x.LastName = kontrol.LastName;
                        x.Password = newPassword.ToString();
                        x.RoleId = kontrol.RoleId;
                        x._Role = kontrol._Role;
                        user.TUpdate(x);
                        TempData["Message"] = "Yeni şifreniz mail adresinize başarıyla gönderilmiştir.";

                    }
                    else
                    {
                        TempData["Message"] = "Mail adresiniz uyuşmuyor kontrol ediniz.";
                    }
                }
            }
            else
            {
                TempData["Message"] = "Lütfen email adresinizi giriniz.";
            }
            return View();
        }
        #endregion


    }
}