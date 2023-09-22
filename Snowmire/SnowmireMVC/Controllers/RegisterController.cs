using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.WebPages;
using MyAccount.Models;

namespace SnowmireMVC.Controllers
{
    public class RegisterController : Controller
    {
        [HttpPost]
        public HttpStatusCode Test(string username, string password)
        {

            if (!username.IsEmpty() && !password.IsEmpty())
            {
                if(password.Length > 3)
                {
                    TestTable testTable = new TestTable()
                    {
                        Username = username,
                        Password = password
                    };

                    using (var context = new SqlDbContext())
                    {
                        context.TestTable.Add(testTable);
                        context.SaveChanges();
                    }
                    return HttpStatusCode.Created;
                }

                //return new HttpStatusCodeResult(HttpStatusCode.Created, "no uzer");
            }
            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "yez uzer");
            return HttpStatusCode.BadRequest;
        }

        [HttpGet]
        public ActionResult Test()
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK, "mmm sexy");
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SqlDbContext())
                {
                    User user = context.Users
                                       .Where(u => u.UserId == model.UserId && u.Password == model.Password)
                                       .FirstOrDefault();

                    if (user != null)
                    {
                        Session["UserName"] = user.UserName;
                        Session["UserId"] = user.UserId;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid User Name or Password");
                        return View(model);
                    }
                }
            }
            else
            {
                return View(model);
            }
        }
    }
}