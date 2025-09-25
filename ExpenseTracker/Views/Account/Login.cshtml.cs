using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page(); // Razor Pages binding works automatically

            // Example login logic
            if (Input.Email == "test@example.com" && Input.Password == "1234")
                return RedirectToPage("/Index");

            ErrorMessage = "Invalid credentials";
            return Page();
        }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
