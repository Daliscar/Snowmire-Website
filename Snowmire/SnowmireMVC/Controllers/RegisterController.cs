using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.Ajax.Utilities;
using MyAccount.Models;
using SnowmireMVC.Enums;

namespace SnowmireMVC.Controllers
{


    public class RegisterController : Controller
    {
        [HttpPost]
        public HttpStatusCode Test(string username, string password, string email)
        {
            NameValueCollection x = Request.Headers;
            string regex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";


            if (!username.IsEmpty() && !password.IsEmpty())
            {
                if(password.Length > 3
                    && username.Length > 3
                    && email.Length > 3)
                {

                    if(!Regex.IsMatch(email, regex))
                    {
                        return (HttpStatusCode)HttpStatusCodes.InvalidEmail;
                    }
                    
                    TestTable testTable = new TestTable()
                    {
                        Username = username,
                        Password = password,
                        Email = email
                    };

                    using (var context = new SqlDbContext())
                    {

                        // Change this please
                        TestTable isDuplicate = context.TestTable.Where(u => u.Username == username).FirstOrDefault();

                        if (isDuplicate != null)
                        {
                            return (HttpStatusCode)HttpStatusCodes.UsernameTaken;
                        }
                        context.TestTable.Add(testTable);
                        context.SaveChanges();
                    }
                    return (HttpStatusCode)HttpStatusCodes.AccountCreated;
                }
                else
                {
                    return (HttpStatusCode)HttpStatusCodes.TooShort;
                }

            }
            return (HttpStatusCode)HttpStatusCodes.UnexpectedError;
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