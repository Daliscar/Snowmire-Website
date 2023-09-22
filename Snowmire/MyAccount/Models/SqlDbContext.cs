using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MyAccount.Models
{
    public class SqlDbContext : DbContext
    {
        //schoolcontext aici? - nu merge momentan, crapa la publish loginul
        public SqlDbContext() : base("MyAccountContext")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public virtual DbSet<TestTable> TestTable { get; set; }
    }
}