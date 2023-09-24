using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyAccount.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime? LastHeartbeatTime { get; set; }
        public virtual UserGameInfo userGameInfo { get; set; }
    }
}