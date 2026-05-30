using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShopping.Models;
using OnlineShopping.Repository.Interface;
using OnlineShopping.ViewModel;
using BCrypt.Net;

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;

        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_userRepo.EmailExist(model.Email))
            {
                ModelState.AddModelError("","This Email is already register");
                return View(model);
            }
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };
            _userRepo.Register(user);
            _userRepo.Save();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userRepo.GetByEmail(model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("","Invalid email or password.");
                return View(model);

            }

            HttpContext.Session.SetInt32("LoginID", user.LoginId);
            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }



    }
}
