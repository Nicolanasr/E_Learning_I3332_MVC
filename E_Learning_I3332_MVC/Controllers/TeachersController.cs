using Microsoft.AspNetCore.Mvc;

namespace E_Learning_I3332_MVC.Controllers
{
    public class TeachersController : Controller
    {
        private readonly E_Learning_I3332_MVCContext _context;
        public TeachersController(E_Learning_I3332_MVCContext context)
        {
            _context = context;
        }
        //GET: Teachers

       
        public async Task<IActionResult> Index()
        {


            return View(await _context.Teacher.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Teacher = await _context
                .FirstOrDefaultAsync(m => m.Teacherid == id);
            if (Teacher == null)
            {
                return NotFound();
            }

            return View(Teacher);
        }

        // GET: Teacher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,SpecializationID,PhoneNumber,Specialization,JoineDate,YearsOfExperience")] T teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(TeachersController);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(TeacherController);
        }

        // GET: Teacher/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(Teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherID,SpecializationID,PhoneNumber,Specialization,JoinedDate,YearsOfExperience")] Teacher teacher)
        {
            if (id != TeacherController.TeacherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(TeacherController);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(TeacherController.TeacherID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(TeacherController);
        }

        // GET: Teacher/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(Teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teacher.FindAsync(id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teacher.Any(e => e.TeacherID == id);
        }
    }
}

