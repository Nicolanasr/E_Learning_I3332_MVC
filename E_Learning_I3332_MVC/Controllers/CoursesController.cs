using E_Learning_I3332_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_I3332_MVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly MySQLDBContext _db;
        [ActivatorUtilitiesConstructorAttribute]
        public CoursesController(MySQLDBContext db)
        {
            _db = db;
        }


        [Authorize]
        [Route("courses")]
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst("userId");
            var role_id = claimsIdentity?.FindFirst("role_id");
            var role = claimsIdentity?.FindFirst("role");
            var TS_Id = claimsIdentity?.FindFirst("TS_Id");

            string? specializationId = null;
            if (_db.Users != null && userId != null)
            {
                if (role?.Value.ToString() == "student" && _db.Students != null)
                {
                    specializationId = _db.Students.FirstOrDefault(s => s.UserId.ToString() == userId.Value.ToString())?.SpecializationId?.ToString();
                }
                else if (role?.Value.ToString() == "teacher" && _db.Teachers != null)
                {
                    specializationId = _db.Teachers.FirstOrDefault(t => t.UserId.ToString() == userId.Value.ToString())?.SpecializationId?.ToString();
                }
            }

            var coursesTeachers = new Dictionary<string, string>();
            List<Courses> AllCourses = new();
            if (_db.Courses != null && specializationId != null && _db.Teaches != null)
            {
                AllCourses = _db.Courses.Where(c => c.SpecializationId.ToString() == specializationId.ToString()).ToList<Courses>();
                var teachesList = _db.Teaches
                                    .Include(t => t.Course)
                                    .Include(t => t.Teacher)
                                    .ThenInclude(t => t.User)
                                    .Where(t => t.Teacher.SpecializationId.ToString() == specializationId.ToString())
                                    .ToList();

                foreach (var item in teachesList)
                {
                    coursesTeachers.Add(item.Course.CourseId.ToString(), item?.Teacher?.User?.FirstName + item?.Teacher?.User?.LastName);
                }

            }

            IEnumerable<int?>? studentEnrolledCourse = null;
            if (role?.Value.ToString() == "student" && _db.StudentCourses != null && TS_Id != null)
            {
                studentEnrolledCourse = _db.StudentCourses.Where(sc => sc.StudentId.ToString() == TS_Id.Value.ToString()).ToList().Select(x => x.CourseId);
            }

            ViewData["coursesTeachers"] = coursesTeachers;
            ViewData["AllCourses"] = AllCourses;
            ViewData["enrolled"] = studentEnrolledCourse;

            return View();
        }

        [Authorize]
        [Route("courses/enroll")]
        [HttpPost]
        public ActionResult Enroll(Courses course)
        {

            var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var stdId = claimsIdentity?.FindFirst("TS_Id");

            if (_db.Courses != null && _db.Students != null && stdId != null && _db.StudentCourses != null)
            {
                StudentCourses? courseEnr = new()
                {
                    Student = _db.Students.Find(int.Parse(stdId.Value)),
                    Course = _db.Courses.Find(course.CourseId),
                    Year = DateTime.Now.Year,
                    Semester = "winter",
                    EntolementDate = DateTime.Now,
                };

                _db.StudentCourses.Add(courseEnr);
                _db.SaveChanges();
            }


            return Redirect("/courses");
        }
    }
}