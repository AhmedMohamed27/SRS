using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SRS.Models
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<AspUser> Users { get; set; }


        public virtual DbSet<AspCourse> Courses { get; set; }

        public virtual DbSet<AspRole> Roles { get; set; }

        public virtual DbSet<AspStudentLvl> StudentLvl { get; set; }

        public virtual DbSet<AspDepartment> Departments { get; set; }

        public virtual DbSet<AspMatrial> Matrials { get; set; }

        public virtual DbSet<AspPreRequisCourse> PreRequisCourse { get; set; }

        public virtual DbSet<AspStuffCourses> StuffCourses { get; set; }

        public virtual DbSet<AspRegistration> Registrations { get; set; }

        public virtual DbSet<AspDoctorCourses> DoctorCourses { get; set; }

        public virtual DbSet<AspGPA> Gpa { get; set; }

        public virtual DbSet<AspCourseTime> CourseTimes { get; set; }


        //public virtual DbSet<AspAssingment> Assingments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}