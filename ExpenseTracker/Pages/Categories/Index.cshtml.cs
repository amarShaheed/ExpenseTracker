using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔑 Make sure this is plural (Categories)
        public IList<Category> Categories { get; set; } = new List<Category>();

        public async Task OnGetAsync()
        {
            // 🔑 This loads all categories from DB
            Categories = await _context.Categories.ToListAsync();
        }
    }
}
