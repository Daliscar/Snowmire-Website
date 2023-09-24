using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyAccount.Models
{
    public class UserGameInfo
    {
        public UserGameInfo()
        {
            Coins = 0;
            Experience = 0;
            Characters = "000";
        }
        [ForeignKey("UserInfo")]
        public int UserGameInfoId { get; set; }
        public string PlayerName { get; set; }
        public int Coins { get; set; }
        public int Experience { get; set; }
        public string Characters { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}