using System.Runtime.InteropServices;
using E_Learning_I3332_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_I3332_MVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly MySQLDBContext _db;

        public object TeacherIDQuery { get; private set; }

        [ActivatorUtilitiesConstructorAttribute]
        public CoursesController(MySQLDBContext db)
        {
            _db = db;
        }


        //[HttpGet]
        //public async Task<IActionResult> Index(string Coursessearch)
        // {
        // ViewData["Getcoursesdetails"]= Coursessearch;  
        //  var coursesquery= from x in _db.Courses select x;
        // if(!String.IsNullOrEmpty(Coursessearch))
        // {
        //    coursesquery = coursesquery.Where(x => x.CourseName.Contains(Coursessearch) || x.CourseName.Contains(Coursessearch));
        // }
        //  return View(await coursesquery.AsNoTracking().ToListAsync());
        // }
        public async Task<IActionResult> Index(int CourseIdint, string searchString)
        {
            IQueryable<int> dridQuery = from m in _db.Courses
                                        orderby m.CourseId
                                        select m.CourseId;

            var operations = from m in _db.Courses
                             select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                operations = operations.Where(o => o.CourseName.Contains(searchString));

            }

            if (CourseIdint != 0)
            {
                operations = operations.Where(op => op.CourseId == CourseIdint);
            }

            var OperationNameList = new 
            {
               // TeacherIds = new SelectList(await TeacherIDQuery.Distinct().ToListAsync()),
                Courses = await operations.ToListAsync()
            };

            return View();
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

            if (role?.Value.ToString() == "student" && _db.StudentCourses != null && TS_Id != null)
            {
                IEnumerable<int?>? studentEnrolledCourse = _db.StudentCourses.Where(sc => sc.StudentId.ToString() == TS_Id.Value.ToString()).ToList().Select(x => x.CourseId);
                ViewData["enrolled"] = studentEnrolledCourse;
            }
            else if (role?.Value.ToString() == "teacher" && _db.Teaches != null && TS_Id != null && _db.StudentCourses != null)
            {
                IEnumerable<int?>? teachersCourses = _db.Teaches.Where(teach => teach.TeacherId.ToString() == TS_Id.Value.ToString()).ToList().Select(x => x.CourseId);
                List<StudentCourses> teachesCoursesDetailsList = _db.StudentCourses
                                                                .Include(sc => sc.Course)
                                                                .Where(a => teachersCourses.Any(b => b == a.CourseId))
                                                                .ToList();

                Dictionary<int?, int> teachesCoursesDetails = new();
                foreach (var item in teachesCoursesDetailsList)
                {
                    if (teachesCoursesDetails.ContainsKey(item.CourseId))
                    {
                        teachesCoursesDetails[item.CourseId] = teachesCoursesDetails[item.CourseId] + 1;
                    }
                    else
                    {
                        teachesCoursesDetails.Add(item.CourseId, 1);
                    }
                }

                ViewData["teachesCoursesDetails"] = teachesCoursesDetails;
                ViewData["teaches"] = teachersCourses;
            }

            ViewData["coursesTeachers"] = coursesTeachers;
            ViewData["AllCourses"] = AllCourses;

            return View();
        }

        [Authorize]
        [Route("courses/enroll")]
        [HttpPost]
        public ActionResult Enroll(Courses course, string semester)
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
                    Semester = semester,
                    EntolementDate = DateTime.Now,
                };

                _db.StudentCourses.Add(courseEnr);
                _db.SaveChanges();
            }

            return Redirect("/courses");
        }


        [Authorize]
        [Route("courses/{course_id}")]
        public ActionResult Details(int course_id)
        {
            var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst("userId");
            // var role_id = claimsIdentity?.FindFirst("role_id");
            var role = claimsIdentity?.FindFirst("role");
            var TS_Id = claimsIdentity?.FindFirst("TS_Id");

            if (userId != null && _db.Courses != null && _db.Teaches != null && _db.StudentCourses != null)
            {
                Courses? courseDetails = _db.Courses
                                        .Include(t => t.Specialization)
                                        .Where(course => course.CourseId == course_id).FirstOrDefault();

                if (courseDetails == null)
                {
                    return Redirect("/courses");
                }

                Teaches? teacherDetails = _db.Teaches
                                        .Include(teach => teach.Teacher)
                                        .ThenInclude(teacher => teacher.User)
                                        .Where(teach => teach.CourseId == course_id).FirstOrDefault();

                ViewData["teacherDetails"] = teacherDetails?.Teacher;

                List<StudentCourses>? enrolledStudents = _db.StudentCourses
                                            .Include(sCourse => sCourse.Student)
                                            .ThenInclude(std => std.User)
                                            .Where(sCourse => sCourse.CourseId == course_id).ToList();

                ViewData["enrolledStudents"] = enrolledStudents;

                // for students
                if (role?.Value.ToString() == "student" && TS_Id?.Value != null && _db.StudentCourses != null)
                {
                    StudentCourses? studentCourseDetails = _db.StudentCourses.Where(sCourse => sCourse.StudentId == int.Parse(TS_Id.Value) && sCourse.CourseId == course_id).FirstOrDefault();
                    ViewData["studentCourseDetails"] = studentCourseDetails;
                }

                ViewData["courseDetails"] = courseDetails;
                return View();
            }

            return Redirect("/courses");
        }
    }
}