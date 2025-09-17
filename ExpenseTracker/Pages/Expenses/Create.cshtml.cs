//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using ExpenseTracker.Data;
//using ExpenseTracker.Models;
//using System.Threading.Tasks;
//using System.Linq;

//namespace ExpenseTracker.Pages.Expenses
//{
//    public class CreateModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;

//        public CreateModel(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Expense Expense { get; set; } = new Expense();

//        public SelectList CategoryOptions { get; set; }

//        public void OnGet()
//        {
//            CategoryOptions = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                CategoryOptions = new SelectList(_context.Categories.ToList(), "CategoryId", "Name");
//                return Page();
//            }

//            _context.Expenses.Add(Expense);
//            await _context.SaveChangesAsync();
//            return RedirectToPage("Index");
//        }
//    }
//}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;   // <-- needed for SelectList
using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Expense Expense { get; set; } = new Expense();

        // Use a name that cannot be confused with DbSet<Categories>
        public SelectList CategoryOptions { get; set; }

        public IActionResult OnGet()
        {
            CategoryOptions = new SelectList(_context.Categories, "CategoryId", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Always repopulate the dropdown before any return
            CategoryOptions = new SelectList(_context.Categories, "CategoryId", "Name");

            if (!ModelState.IsValid)
            {
                // Optional: surface model binding errors to help you debug quickly
                foreach (var kv in ModelState)
                {
                    foreach (var err in kv.Value.Errors)
                    {
                        ModelState.AddModelError(string.Empty, $"Field '{kv.Key}': {err.ErrorMessage}");
                    }
                }
                return Page();
            }

            _context.Expenses.Add(Expense);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
