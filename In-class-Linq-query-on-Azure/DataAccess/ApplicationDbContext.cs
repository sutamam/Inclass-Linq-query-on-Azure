using In_class_Linq_query_on_Azure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace In_class_Linq_query_on_Azure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Students> students { get; set; }
        public DbSet<Courses> courses { get; set; }
        public DbSet<StudentCourseEnrollment> studentCourseEnrollment { get; set; }

    }
}
