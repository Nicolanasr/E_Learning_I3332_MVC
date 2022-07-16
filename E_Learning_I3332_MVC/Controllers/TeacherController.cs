using E_Learning_I3332_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning_I3332_MVC.Controllers

{
    public class TeacherController : Controller
    {

        private readonly MySQLDBContext _db;
        [ActivatorUtilitiesConstructorAttribute]
        public TeacherController(MySQLDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
