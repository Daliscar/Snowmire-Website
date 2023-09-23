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


    public class MobaController : Controller
    {
        [HttpPost]
        public int Register(string username, string password, string email)
        {


            if(Request.Headers["sEcUrItY"] != "sEcUrItY")
            {
                return (int)HttpStatusCodes.NoAuthority;
            }
            string regex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";

            if (!username.IsEmpty() && !password.IsEmpty())
            {
                if(password.Length > 3
                    && username.Length > 3
                    && email.Length > 3)
                {

                    if(!Regex.IsMatch(email, regex))
                    {
                        return (int)HttpStatusCodes.InvalidEmail;
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
                            return (int)HttpStatusCodes.UsernameTaken;
                        }
                        context.TestTable.Add(testTable);
                        context.SaveChanges();
                    }
                    return (int)HttpStatusCodes.AccountCreated;
                }
                else
                {
                    return (int)HttpStatusCodes.TooShort;
                }

            }
            return (int)HttpStatusCodes.UnexpectedError;
        }

        [HttpPost]
        public string Login()
        {
            string token = Guid.NewGuid().ToString();
            string response = $"{{\r\n  \"token\": \"{token}\",\r\n  \"response\": \"connected\"\r\n}}";

            if (Request.Headers["sEcUrItY"] != "sEcUrItY")
            {
                return "No Authority!";
            }
            return response;
        }

    }
}