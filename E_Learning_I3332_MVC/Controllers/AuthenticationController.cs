using System.Diagnostics;
using System.Security.Claims;
using E_Learning_I3332_MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning_I3332_MVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly MySQLDBContext _db;
    [ActivatorUtilitiesConstructorAttribute]
    public AuthenticationController(MySQLDBContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {
        return Redirect("~/authentication/login");
    }

    [Route("authentication/login")]
    public IActionResult Login(string ReturnUrl)
    {
        TempData["ReturnUrl"] = ReturnUrl ?? "/";
        return View();
    }

    [Route("authentication/register_student")]
    public IActionResult RegisterStudent(string UserId)
    {
        if (UserId == null)
        {
            return Redirect("/");
        }
        if (_db.Specializations != null)
        {
            List<Specializations> specializationsList = _db.Specializations.ToList();

            ViewData["specializations"] = specializationsList;
            TempData["UserId"] = UserId;

            return View("register_student");
        }

        return Redirect("/");
    }

    [Route("authentication/register")]
    public IActionResult Register(string ReturnUrl)
    {
        TempData["ReturnUrl"] = ReturnUrl ?? "/";
        return View();
    }

    [Route("authentication/register")]
    [HttpPost]
    public IActionResult RegisterSave(Users user)
    {
        if (_db.Roles != null)
        {
            user.Role = _db.Roles.FirstOrDefault(r => r.Name == "student");
        }

        if (user.Role != null)
        {
            _db.Add<Users>(user);
            _db.SaveChanges();

            return Redirect("/");
        }

        return View("register", user);
    }

    [Route("authentication/login")]
    [HttpPost]
    public async Task<IActionResult> LoginSave(Users user, string ReturnUrl)
    {
        if (_db.Users != null)
        {
            Users? userDb = _db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (userDb != null)
            {
                if (userDb.Status == 0)
                {
                    string register_user_url = "/authentication/register_student?UserId=" + userDb.Id.ToString();

                    return Redirect(register_user_url);
                }

                Roles? role = null;
                if (_db.Roles != null)
                {
                    role = _db.Roles.Find(userDb.RoleId);
                }

                var claims = new List<Claim>
                {
                    new Claim("userId", userDb.Id.ToString() ?? ""),
                    new Claim("email", userDb.Email ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("role", role?.Name ?? ""),
                    new Claim("role_id", userDb.RoleId.ToString()),
                };

                // student
                if (role?.Name?.ToString() == "student" && _db.Students != null)
                {
                    Students? foundStd = _db.Students.FirstOrDefault(std => std.UserId.ToString() == userDb.Id.ToString());
                    claims.Add(new Claim("TS_Id", foundStd?.StudentId.ToString() ?? ""));
                }
                // teacher
                else if (role?.Name?.ToString() == "teacher" && _db.Teachers != null)
                {
                    Teachers? foundTeacher = _db.Teachers.FirstOrDefault(std => std.UserId.ToString() == userDb.Id.ToString());
                    claims.Add(new Claim("TS_Id", foundTeacher?.TeacherId.ToString() ?? ""));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(ReturnUrl ?? "/");
            }
            else
            {
                TempData["error"] = "Email or password does not match!";
                return View("login");
            }

        }
        return Redirect("/");
    }


    [Route("authentication/register_student")]
    [HttpPost]
    public async Task<IActionResult> RegisterStudentSave(Students student, string SpecializationId, string UserId)
    {

        if (_db.Specializations != null && SpecializationId != null)
        {
            student.Specialization = _db.Specializations.Find(int.Parse(SpecializationId));
        }


        Users? user = null;
        if (_db.Users != null && UserId != null)
        {
            user = _db.Users.Find(int.Parse(UserId));
            student.User = user;
            if (user != null)
            {
                user.Status = 1;
                _db.Users.Update(user);
            }
        }

        if (student != null)
        {
            student.JoinedDate = DateTime.Now;
            _db.Add<Students>(student);
            _db.SaveChanges();

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("userId", user.Id.ToString() ?? ""),
                    new Claim("stdId", student.StudentId.ToString() ?? ""),
                    new Claim("email", user.Email ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("role", "student"),
                    new Claim("role_id", user.RoleId.ToString()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
            }
        }

        return Redirect("/");
    }


    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/authentication/login");
    }


    [Route("authentication/forgot-password")]
    [ActionName("forgot-password")]
    public ActionResult ForgotPassword()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
