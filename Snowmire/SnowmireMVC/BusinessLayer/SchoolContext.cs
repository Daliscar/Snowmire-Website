using SnowmireMVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SnowmireMVC.BusinessLayer
{
    public class SchoolContext : DbContext
    {

        public SchoolContext() : base("SchoolContext")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}