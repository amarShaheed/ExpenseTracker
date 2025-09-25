using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExpenseTracker.Models;

namespace ExpenseTracker.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty] // important for POST binding
        public RegisterViewModel Input { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
            // Initialization logic (if any)
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // validation errors, stay on the same page
            }

            // TODO: Save user registration here

            // Redirect to Login page after success
            TempData["SuccessMessage"] = "Registration successful!";
            return RedirectToPage("/Account/Login");
        }
    }
}