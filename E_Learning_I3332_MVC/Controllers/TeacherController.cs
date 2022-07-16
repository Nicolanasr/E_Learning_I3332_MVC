using Microsoft.AspNetCore.Mvc;

namespace E_Learning_I3332_MVC.Controllers
    
{
    public class TeacherController : Controller
    { 

       
        public IActionResult Index()
        {
            return View();
        }
    }  
}
