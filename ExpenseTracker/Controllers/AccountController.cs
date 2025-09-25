//using ExpenseTracker.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace ExpenseTracker.Controllers
//{
//    public class AccountController : Controller
//    {
//        // GET: /Account/Login
//        [HttpGet]
//        public IActionResult Login() => View();

//        // POST: /Account/Login
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Login(LoginViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            // TODO: Replace with real authentication
//            if (model.Email == "test@example.com" && model.Password == "123")
//            {
//                TempData["Success"] = "Login successful!";
//                return RedirectToAction("Index", "Home");
//            }

//            ModelState.AddModelError("", "Invalid login attempt.");
//            return View(model);
//        }

//        // GET: /Account/Register
//        [HttpGet]
//        public IActionResult Register() => View();

//        // POST: /Account/Register
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Register(RegisterViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            // TODO: Save user to database here
//            TempData["Success"] = "Registration successful. Please login.";
//            return RedirectToAction("Login");
//        }

//        public IActionResult Logout()
//        {
//            // TODO: clear session or identity cookie
//            return RedirectToAction("Login");
//        }
//    }
//}

using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Dummy check - replace with database later
            if (model.Email == "test@example.com" && model.Password == "1234")
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // TODO: Save to database later

            TempData["SuccessMessage"] = "Registration successful! Please login.";
            return RedirectToAction("Login");
        }
    }
}