using library.Models;
using library.Services;
using library.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace library.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public AccountController(ILibraryService libraryService,
            UserManager<User> userManager, SignInManager<User> signInManager)
            : base(libraryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
                return View("Login", user);

            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.UserPassword, user.RememberLogin, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Wrong username or password.");
                return View("Login", user);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel user)
        {
            if (!ModelState.IsValid)
                return View("Register", user);

            User newUser = new User
            {
                UserName = user.UserName,
                Name = user.Name,
                Email = user.UserEmail,
                Address = user.UserAddress,
                PhoneNumber = user.UserPhoneNumber
            };
            var result = await _userManager.CreateAsync(newUser, user.UserPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View("Register", user);
            }

            await _signInManager.SignInAsync(newUser, false);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}