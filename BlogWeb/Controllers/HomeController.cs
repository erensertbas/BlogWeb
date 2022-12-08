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

namespace BlogWeb.PL.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AboutUsRepository aboutUs = new AboutUsRepository();
        ContactRepository contact = new ContactRepository();
        CategoryRepository category = new CategoryRepository();
        BlogRepository blog = new BlogRepository();
       // SubscriberRepository subscriber = new BL.Repository.SubscriberRepository();
        Context c = new Context();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SubscriberAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubscriberAdd(Subscriber sub)
        {
            //if (ModelState.IsValid)
            //{
            //    aboutUs.TAdd();
            //    TempData["EklemeSonuc"] = 1;
            //    return RedirectToAction("AboutUs");
            //}
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
                return View(result.Distinct().OrderByDescending(d=>d.Date));
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

            if (ModelState.IsValid)
            {
                MimeMessage mimeMessage = new MimeMessage();

                MailboxAddress mailboxAddressFrom = new MailboxAddress("Destek", "cutopya@gmail.com");

                mimeMessage.From.Add(mailboxAddressFrom);

                MailboxAddress mailboxAddressTo = new MailboxAddress("Admin", "info@cutopya.com");
                mimeMessage.To.Add(mailboxAddressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = "Maili Gönderen : " + model.Firstname + " " + model.Lastname + "\n" + " Maili : " + model.Email + "\n" + model.Content;
                mimeMessage.Body = bodyBuilder.ToMessageBody();

                mimeMessage.Subject = model.Topic;

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 25, false);
                client.Authenticate("cutopya@gmail.com", "zstvndudhopkukec");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }
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
    }
}