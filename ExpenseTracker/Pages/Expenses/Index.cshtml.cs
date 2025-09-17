using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Expense> Expenses { get; set; } = new List<Expense>();

        public async Task OnGetAsync()
        {
            Expenses = await _context.Expenses
                                     .Include(e => e.Category)
                                     .ToListAsync();
        }
    }
}
