using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Learning_I3332_MVC.Data;
using E_Learning_I3332_MVC.Models;

namespace E_Learning_I3332_MVC.Controllers
{
    public class StudentsController : Controller

    {
        private readonly MySQLDBContext _db;
        [ActivatorUtilitiesConstructorAttribute]
        public StudentsController(MySQLDBContext db)
        {
            _db = db;
        }
        //GET: Student
        public IActionResult Index()
        {
            return View();
            // await _context.Student.ToListAsync()
        }

        // GET: Student/Details/5
        [Authorize]
        [Route("student/{student_id}")]
        public ActionResult Details(int student_id)
        {
            if (_db.Students != null)
            {
                Students? studentDetails = _db.Students
                                            .Include(Student => Student.User)
                                            .Include(Student => Student.Specialization)
                                            .Where(Student => Student.StudentId == student_id).FirstOrDefault();

                if (studentDetails == null)
                {
                    return NotFound();
                }
                ViewData["studentDetails"] = studentDetails;

                return View();
            }

            return Redirect("/");
        }
    }
}

internal class AuthorizeAttribute : Attribute
{ }
