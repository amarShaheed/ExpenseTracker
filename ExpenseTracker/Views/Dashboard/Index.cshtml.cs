//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using ExpenseTracker.Data;
//using ExpenseTracker.Models;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace ExpenseTracker.Pages.Expenses
//{
//    public class IndexModel : PageModel
//    {
//        private readonly ApplicationDbContext _context;

//        public IndexModel(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // ✅ Must match what you use in Razor: "Model.Expenses"
//        public IList<Expense> Expenses { get; set; } = default!;

//        public async Task OnGetAsync()
//        {
//            Expenses = await _context.Expenses
//                                     .Include(e => e.Category) // Important for e.Category.Name
//                                     .ToListAsync();
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Strongly typed properties instead of ViewBag
        public decimal TotalExpenses { get; set; }
        public List<CategoryTotal> CategoryTotals { get; set; } = new();
        public List<DailyTotal> DailyTrend { get; set; } = new();

        public async Task OnGetAsync()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            TotalExpenses = await _context.Expenses
                .Where(e => e.ExpenseDate >= startOfMonth)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            CategoryTotals = await _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => e.Category.Name)
                .Select(g => new CategoryTotal { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToListAsync();

            DailyTrend = await _context.Expenses
                .GroupBy(e => e.ExpenseDate.Date)
                .Select(g => new DailyTotal { Date = g.Key, Total = g.Sum(e => e.Amount) })
                .OrderBy(d => d.Date)
                .ToListAsync();
        }

        public class CategoryTotal
        {
            public string Category { get; set; }
            public decimal Total { get; set; }
        }

        public class DailyTotal
        {
            public DateTime Date { get; set; }
            public decimal Total { get; set; }
        }
    }
}
