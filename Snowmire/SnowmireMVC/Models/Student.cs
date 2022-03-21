using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnowmireMVC.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }

        //public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}