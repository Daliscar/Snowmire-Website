using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MyAccount.Models
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("MyAccountContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}