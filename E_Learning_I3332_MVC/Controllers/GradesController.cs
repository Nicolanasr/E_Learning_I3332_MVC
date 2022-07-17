using System.Diagnostics;
using E_Learning_I3332_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_I3332_MVC.Controllers;

public class GradesController : Controller
{
    private readonly MySQLDBContext _db;
    [ActivatorUtilitiesConstructorAttribute]
    public GradesController(MySQLDBContext db)
    {
        _db = db;
    }

    [Authorize]
    public IActionResult Index()
    {
        var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
        var role = claimsIdentity?.FindFirst("role");
        var TS_Id = claimsIdentity?.FindFirst("TS_Id");

        if (role?.Value == "teacher" && TS_Id != null && _db.Teaches != null)
        {
            List<Teaches> coursesByTeacher = _db.Teaches
                .Include(teach => teach.Course)
                .Where(teach => teach.TeacherId == int.Parse(TS_Id.Value))
                .ToList();

            ViewData["coursesByTeacher"] = coursesByTeacher;
        }
        else if (role?.Value == "student" && TS_Id != null && _db.StudentCourses != null)
        {
            List<StudentCourses> enrolledCourses = _db.StudentCourses
                                                    .Include(sc => sc.Course)
                                                    .Where(sc => sc.StudentId == int.Parse(TS_Id.Value))
                                                    .ToList();

            ViewData["enrolledCourses"] = enrolledCourses;
        }

        return View();
    }

    [Authorize]
    [Route("grades/{courseId}")]
    public IActionResult GradesByCourse(int courseId)
    {
        var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
        var role = claimsIdentity?.FindFirst("role");
        var TS_Id = claimsIdentity?.FindFirst("TS_Id");

        if (role?.Value == "teacher" && TS_Id != null && _db.StudentCourses != null && _db.Teaches != null)
        {

            Teaches? teacherCourses = _db.Teaches
                                        .Where(teach => teach.TeacherId == int.Parse(TS_Id.Value) && teach.CourseId == courseId)
                                        .FirstOrDefault();

            if (teacherCourses != null)
            {
                List<StudentCourses> studentsEnrolled = _db.StudentCourses
                                                            .Include(sc => sc.Course)
                                                            .Include(sc => sc.Student)
                                                            .ThenInclude(student => student.User)
                                                            .Include(sc => sc.StdGrades)
                                                            .Where(sc => sc.CourseId == courseId)
                                                            .ToList();

                ViewData["studentsEnrolled"] = studentsEnrolled;
                return View();
            }
        }
        else if (role?.Value == "student" && TS_Id != null && _db.StudentCourses != null && _db.Teaches != null)
        {
            List<StudentCourses> studentGrades = _db.StudentCourses
                                                        .Include(sc => sc.Course)
                                                        .Include(sc => sc.Student)
                                                        .ThenInclude(student => student.User)
                                                        .Include(sc => sc.StdGrades)
                                                        .Where(sc => sc.CourseId == courseId && sc.StudentId == int.Parse(TS_Id.Value))
                                                        .ToList();

            ViewData["studentsEnrolled"] = studentGrades;
            return View();
        }

        return Redirect("/grades");

    }

    [Authorize]
    [Route("grades/add")]
    [HttpPost]
    public IActionResult AddGradeToCourse(Grades grades, string returnUrl)
    {
        var claimsIdentity = User.Identity as System.Security.Claims.ClaimsIdentity;
        var role = claimsIdentity?.FindFirst("role");
        var TS_Id = claimsIdentity?.FindFirst("TS_Id");

        if (role?.Value == "teacher" && TS_Id != null && _db.Grades != null && _db.StudentCourses != null && _db.Teaches != null)
        {
            StudentCourses? stdCourse = _db.StudentCourses.Find(grades.StudentCourseId);
            if (stdCourse != null)
            {
                Teaches? teacherCourse = _db.Teaches
                                            .Where(teach => teach.TeacherId == int.Parse(TS_Id.Value) && teach.CourseId == stdCourse.CourseId)
                                            .FirstOrDefault();

                if (teacherCourse != null)
                {
                    _db.Grades.Add(grades);
                    _db.SaveChanges();
                }
            }
        }

        return Redirect(returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
