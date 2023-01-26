using BlogWeb.BL.Repository;
using BlogWeb.DL.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogWeb.PL.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        UserRepository user = new UserRepository();
        FirebaseAuthProvider auth;


        public void SetCookie(string key, string id)
        {
            HttpContext.Response.Cookies.Append(key, id);
        }

        public string GetCookie(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string id);
            return id;
        }


        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
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
                    var userIdentity = new ClaimsIdentity(claims,"a");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);

                    SetCookie("userId", dataValue.UserId.ToString());


                    return RedirectToAction("Index","Admin");
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
                    return RedirectToAction("Index", "User");
                }
              
                
               
            }
            else
            {
                TempData["ErrorMessage"] = "Kullanıcı email ya da şifre girmediniz lütfen kontrol ediniz.";
             
            }
            return View();




        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserModel Model) // kayıt ol modeli
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
    }
}
