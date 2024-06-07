using DataAccess;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using TriviaApp.Services;

namespace TriviaApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model, string password)
        {
            if (ModelState.IsValid)
            {
                _userService.RegisterUser(model, password);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = _userService.LoginUser(username, password);
            if (user != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
