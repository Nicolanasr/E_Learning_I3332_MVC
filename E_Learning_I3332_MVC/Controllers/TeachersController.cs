using E_Learning_I3332_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_I3332_MVC.Controllers
{
    public class TeachersController : Controller
    {
        private readonly MySQLDBContext _db;
        [ActivatorUtilitiesConstructorAttribute]
        public TeachersController(MySQLDBContext db)
        {
            _db = db;
        }

        //GET: Teachers
        public IActionResult Index()
        {
            return View();
            // await _context.Teacher.ToListAsync()
        }

        // GET: Teachers/Details/5
        [Authorize]
        [Route("teachers/{teacher_id}")]
        public ActionResult Details(int teacher_id)
        {
            if (_db.Teachers != null && _db.Teaches != null)
            {
                Teachers? teacherDetails = _db.Teachers
                                            .Include(teacher => teacher.User)
                                            .Include(teacher => teacher.Specialization)
                                            .Where(teacher => teacher.TeacherId == teacher_id).FirstOrDefault();

                if (teacherDetails == null)
                {
                    return NotFound();
                }
                ViewData["teacherDetails"] = teacherDetails;

                List<Teaches>? teachingCourses = _db.Teaches
                                            .Include(teaches => teaches.Course)
                                            .Where(teacher => teacher.TeacherId == teacher_id).ToList();

                if (teacherDetails == null)
                {
                    return NotFound();
                }

                ViewData["teacherDetails"] = teacherDetails;
                ViewData["teachingCourses"] = teachingCourses;

                return View();
            }

            return Redirect("/");
        }

        //     // GET: Teacher/Create
        //     public IActionResult Create()
        //     {
        //         return View();
        //     }

        //     // POST: Teacher/Create
        //     [HttpPost]
        //     [ValidateAntiForgeryToken]
        //     public async Task<IActionResult> Create(Teachers teacher)
        //     {
        //         // if (ModelState.IsValid)
        //         // {
        //         //     _context.Add(TeachersController);
        //         //     await _context.SaveChangesAsync();
        //         //     return RedirectToAction(nameof(Index));
        //         // }
        //         return View(TeacherController);
        //     }

        //     // GET: Teacher/Edit/5
        //     public async Task<IActionResult> Edit(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return NotFound();
        //         }

        //         var teacher = await _context.Teacher.FindAsync(id);
        //         if (teacher == null)
        //         {
        //             return NotFound();
        //         }
        //         return View(Teacher);
        //     }

        //     // POST: Teacher/Edit/5
        //     [HttpPost]
        //     [ValidateAntiForgeryToken]
        //     public async Task<IActionResult> Edit(int id, [Bind("TeacherID,SpecializationID,PhoneNumber,Specialization,JoinedDate,YearsOfExperience")] Teachers teacher)
        //     {
        //         if (id != TeacherController.TeacherID)
        //         {
        //             return NotFound();
        //         }

        //         if (ModelState.IsValid)
        //         {
        //             try
        //             {
        //                 _context.Update(TeacherController);
        //                 await _context.SaveChangesAsync();
        //             }
        //             catch (DbUpdateConcurrencyException)
        //             {
        //                 if (!TeacherExists(TeacherController.TeacherID))
        //                 {
        //                     return NotFound();
        //                 }
        //                 else
        //                 {
        //                     throw;
        //                 }
        //             }
        //             return RedirectToAction(nameof(Index));
        //         }
        //         return View(TeacherController);
        //     }

        //     // GET: Teacher/Delete/5
        //     public async Task<IActionResult> Delete(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return NotFound();
        //         }

        //         var teacher = await _context.Teacher
        //             .FirstOrDefaultAsync(m => m.TeacherID == id);
        //         if (teacher == null)
        //         {
        //             return NotFound();
        //         }

        //         return View(Teacher);
        //     }

        //     // POST: Teacher/Delete/5
        //     [HttpPost, ActionName("Delete")]
        //     [ValidateAntiForgeryToken]
        //     public async Task<IActionResult> DeleteConfirmed(int id)
        //     {
        //         var teacher = await _context.Teacher.FindAsync(id);
        //         _context.Teacher.Remove(teacher);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }

        //     private bool TeacherExists(int id)
        //     {
        //         return _context.Teacher.Any(e => e.TeacherID == id);
        //     }
    }
}

