using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyAccount.Infrastructure;
using SnowmireMVC.Models;

namespace SnowmireMVC.Controllers
{
    //[CustomAuthenticationFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize("SuperAdmin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [CustomAuthorize("Admin", "SuperAdmin", "User")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Contact(EmailFormModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //        var message = new MailMessage();
        //        message.To.Add(new MailAddress("sirsafari@hotmail.com"));  // replace with valid value 
        //        message.From = new MailAddress("gabidrake@hotmail.com");  // replace with valid value
        //        message.Subject = "This is a test email message sent by an application. ";
        //        message.Body = /*string.Format(body, model.FromName, model.FromEmail, model.Message);*/"This is a test email message sent by an application. "; 
        //        message.IsBodyHtml = true;

        //        using (var smtp = new SmtpClient())
        //        {
        //            var credential = new NetworkCredential
        //            {
        //                UserName = "Sirsafari@hotmail.com",  // replace with valid value
        //                Password = "Daliscar552200"  // replace with valid value
        //            };
        //            smtp.Credentials = credential;
        //            smtp.Host = "smtp-mail.outlook.com";
        //            smtp.Port = 587;
        //            smtp.EnableSsl = true;
        //            await smtp.SendMailAsync(message);
        //            return RedirectToAction("Sent");
        //        }
        //    }
        //    return View(model);
        //}
        public ActionResult UnAuthorized()
        {
            ViewBag.Message = "Un Authorized Page!";

            return View();
        }
    }
}