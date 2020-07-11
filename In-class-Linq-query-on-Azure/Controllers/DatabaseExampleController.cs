using In_class_Linq_query_on_Azure.DataAccess;
using In_class_Linq_query_on_Azure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace In_class_Linq_query_on_Azure.Controllers
{
    public class DatabaseExampleController : Controller
    {
        public ApplicationDbContext dbContext;

        public DatabaseExampleController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> DatabaseOperations()
        {
            Students student = new Students();
            student.StudentId = 1;
            student.StudentName = "Sutama";
            dbContext.Add(student);

            student = new Students();
            student.StudentId = 2;
            student.StudentName = "Shiva";
            dbContext.Add(student);

            student = new Students();
            student.StudentId = 3;
            student.StudentName = "Simon";
            dbContext.Add(student);

            student = new Students();
            student.StudentId = 4;
            student.StudentName = "Aurora";
            dbContext.Add(student);

            Courses course = new Courses();
            course.CourseId = 1;
            course.CourseName = "Distibuted Information System";
            dbContext.Add(course);

            course = new Courses();
            course.CourseId = 2;
            course.CourseName = "Database";
            dbContext.Add(course);

            course = new Courses();
            course.CourseId = 3;
            course.CourseName = "Distributed Management";
            dbContext.Add(course);

            course = new Courses();
            course.CourseId = 4;
            course.CourseName = "Advance System Analysis";
            dbContext.Add(course);

            course = new Courses();
            course.CourseId = 5;
            course.CourseName = "Data Mining";
            dbContext.Add(course);

            course = new Courses();
            course.CourseId = 6;
            course.CourseName = "Python Class";
            dbContext.Add(course);

            StudentCourseEnrollment sce = new StudentCourseEnrollment();
            sce.StudentId = 1;
            sce.CourseId = 1;
            dbContext.Add(sce);

            sce = new StudentCourseEnrollment();
            sce.StudentId = 1;
            sce.CourseId = 2;
            dbContext.Add(sce);

            sce = new StudentCourseEnrollment();
            sce.StudentId = 1;
            sce.CourseId = 3;
            dbContext.Add(sce);

            sce = new StudentCourseEnrollment();
            sce.StudentId = 2;
            sce.CourseId = 3;
            dbContext.Add(sce);

            sce = new StudentCourseEnrollment();
            sce.StudentId = 2;
            sce.CourseId = 4;
            dbContext.Add(sce);

            sce = new StudentCourseEnrollment();
            sce.StudentId = 3;
            sce.CourseId = 4;
            dbContext.Add(sce);

            dbContext.SaveChanges();

            // READ operation

            var list = dbContext.students
                                .Join(dbContext.studentCourseEnrollment,
                                     c => c.StudentId,
                                     p => p.StudentId,
                                     (c, p) => new { 
                                         StudentName = c.StudentName 
                                     }).GroupBy(info => info.StudentName)
                                 .Select(Group => new
                                    {
                                        StudentName = Group.Key,
                                        Count = Group.Count()
                                    }).OrderByDescending(x => x.Count).First();

            ViewBag.StudentEnrolledForMaxCourses = list.StudentName + " has enrolled for the maximun nymber of courses";

            var courses = dbContext.courses
                                .Join(dbContext.studentCourseEnrollment,
                                     c => c.CourseId,
                                     p => p.CourseId,
                                     (c, p) => new {
                                         CourseName = c.CourseName
                                     }).GroupBy(info => info.CourseName)
                                 .Select(Group => new
                                 {
                                     CourseName = Group.Key,
                                     Count = Group.Count()
                                 }).OrderByDescending(x => x.Count).First();

            ViewBag.MaxEnrolledCourse = courses.CourseName + " has maximun enrollment";

            var notEnrolledCourseList = dbContext.courses
                                .Join(dbContext.studentCourseEnrollment,
                                     c => c.CourseId,
                                     p => p.CourseId,
                                     (c, p) => new {
                                         CourseName = c.CourseName
                                     }).GroupBy(info => info.CourseName)
                                 .Select(Group => new
                                 {
                                     CourseName = Group.Key,
                                     Count = Group.Count()
                                 }).OrderBy(x => x.Count).First();
        
            ViewBag.MinEnrolledCourse = notEnrolledCourseList.CourseName + " has minimun enrollment";


            return View();


        }

    }
}
