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
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Org.BouncyCastle.Crypto.Macs;
using Firebase.Auth;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace BlogWeb.PL.Controllers
{

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

        public IActionResult Index()
        {
            return View();
        }

        #region home

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
        public IActionResult BlogDetail(int id)
        {
            var result = blog.TGet(id);
            return View(result);
        }

        public ActionResult HomeCategory(int id)
        {
            var cat = category.TGet(id).CategoryName;
            TempData["Kategori"] = cat;

            var result = c.Blog.Where(x => x.CategoryId == id).ToList();
            // var result = c.Blog.Distinct().OrderByDescending(d => d.Date);
            if (result.Count() > 0)
            {
                return View(result.Distinct().OrderByDescending(d => d.Date));
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
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
        public IActionResult SignIn()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                //var result = HttpContext.Session.GetInt32("_UserToken").Value;
                //HttpContext.Session.SetInt32("UserID", result);
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserModel loginModel)
        {
                try
                {
                    //log in an existing user
                    var fbAuthLink = await auth
                                    .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);

                    string token = fbAuthLink.FirebaseToken;
                    //save the token to a session variable
                    if (token != null)
                    {
                        var result = c.User.Where(x => x.Email == loginModel.Email).FirstOrDefault();

                        if (result.RoleId == 1)
                        {
                            List<Claim> claimss = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginModel.Email),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
                            ClaimsIdentity claimsIdentitys = new ClaimsIdentity(claimss, CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties propertiess = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentitys), propertiess);

                        }
                        else
                        {
                            List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, loginModel.Email),
                        new Claim(ClaimTypes.Role, "User")
                    };
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                        }

                        HttpContext.Session.SetInt32("_UserToken", result.RoleId);

                        return RedirectToAction("Index");
                    }

                }
                catch (FirebaseAuthException ex)
                {
                    var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                    TempData["Error"] = "Mail adresi veya şifre hatalı!";
                    return View(loginModel);
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
                //ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}