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
using System.Web.UI;
using System.Web.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace SnowmireMVC.Controllers
{
    public class MobaController : Controller
    {
        #region POST

        #region Register
        [HttpPost]
        public int Register(string username, string password, string email)
        {
            
            if (Request.Headers["sEcUrItY"] != "sEcUrItY")
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

                    //Hash+Salt Pass
                    string salt = Crypto.GenerateSalt(16);
                    string pass_Salted = salt + password;
                    string hash = HashPassword(pass_Salted);

                    UserGameInfo userGameInfo = new UserGameInfo()
                    {
                        PlayerName = String.Empty
                    };

                    UserInfo UserInfo = new UserInfo()
                    {
                        Username = username,
                        Hash = hash,
                        Salt = salt,
                        Email = email,
                        userGameInfo = userGameInfo
                    };

                    using (var context = new SqlDbContext())
                    {
                        if (context.UserInfo.Where(u => u.Username == username).FirstOrDefault() != null)
                        {
                            return (int)HttpStatusCodes.UsernameTaken;
                        }
                        context.UserInfo.Add(UserInfo);
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
        #endregion

        #region Login
        [HttpPost]
        public string Login(string username, string password)
        {

            if (Request.Headers["sEcUrItY"] != "sEcUrItY")
            {
                return CreateResponse("No Authority!", String.Empty);
            }


            using (var context = new SqlDbContext())
            {
                UserInfo user = context.UserInfo.Where(x => x.Username == username).FirstOrDefault();

                if (user != null)
                {
                    if (user.Hash == HashPassword(user.Salt + password))
                    {

                        string token = Guid.NewGuid().ToString();
                        user.Token = token;
                        user.LastHeartbeatTime = DateTime.UtcNow;
                        context.SaveChanges();

                        if (user.userGameInfo.PlayerName.IsEmpty())
                        {
                            return CreateResponse("Connected-NameChange", token);
                        };
                        return CreateResponse("Connected", token);
                        

                    }
                    else
                    {
                        return CreateResponse("Invalid Password", String.Empty);
                    }

                }
                return CreateResponse("Invalid Username", String.Empty);
            }

        }
        #endregion

        #region SetPlayerName
        [HttpPost]
        public string SetPlayerName(string token, string newPlayerName)
        {
            if (Request.Headers["sEcUrItY"] != "sEcUrItY")
            {
                return CreateResponse("No Authority!", String.Empty);
            }


            using (var context = new SqlDbContext())
            {
                UserInfo user = context.UserInfo.Where(x => x.Token == token).FirstOrDefault();

                user.userGameInfo.PlayerName = newPlayerName;
                context.SaveChanges();
                return "Name was set";
            }
        }
        #endregion

        #endregion

        #region GET
        #region HeartBeat
[HttpGet]
        public void HeartBeat(string token)
        {
            if (Request.Headers["sEcUrItY"] != "sEcUrItY")
            {
                return;
            }

            using (var context = new SqlDbContext())
            {
                UserInfo user = context.UserInfo.Where(x => x.Token == token).FirstOrDefault();

                
            }
        }
        #endregion

        #region GetPlayerDataByToken
        public string GetPlayerDataByToken(string token)
        {
            if (Request.Headers["sEcUrItY"] != "sEcUrItY")
            {
                return CreateResponse("No Authority!", String.Empty);
            }


            using (var context = new SqlDbContext())
            {
                UserInfo user = context.UserInfo.Where(x => x.Token == token).FirstOrDefault();

                UserGameInfo userModel = new UserGameInfo()
                {
                    PlayerName = user.userGameInfo.PlayerName,
                    Coins = user.userGameInfo.Coins,
                    Experience = user.userGameInfo.Experience,
                    Characters = user.userGameInfo.Characters

                };

                string xy = Newtonsoft.Json.JsonConvert.SerializeObject(userModel);

                return xy;
            }
        }
        #endregion
        #endregion

        #region Helpers
        #region CreateResponse
        private string CreateResponse(string status, string token)
        {
            return $"{{\r\n  \"token\": \"{token}\",\r\n  \"response\": \"{status}\"\r\n}}";
        }
        #endregion

        #region HashPassword
        public string HashPassword(string cryptoPass)
        {
            var SHA = SHA256.Create();

            var asByteArray = Encoding.Default.GetBytes(cryptoPass);
            var hashedPassword = SHA.ComputeHash(asByteArray);
            string storedPassword = Convert.ToBase64String(hashedPassword);
            return storedPassword;
        }
        #endregion
        #endregion
    }
}