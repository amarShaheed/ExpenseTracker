using ExpenseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            var totalExpenses = await _context.Expenses
                .Where(e => e.ExpenseDate >= startOfMonth)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            var categoryTotals = await _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => e.Category.Name)
                .Select(g => new CategoryTotal { Category = g.Key, Total = g.Sum(e => e.Amount) })
                .ToListAsync();

            var dailyTrend = await _context.Expenses
                .GroupBy(e => e.ExpenseDate.Date)
                .Select(g => new DailyTotal { Date = g.Key, Total = g.Sum(e => e.Amount) })
                .OrderBy(d => d.Date)
                .ToListAsync();

            // Pass values to the view
            ViewBag.TotalExpenses = totalExpenses;
            ViewBag.CategoryTotals = categoryTotals;
            ViewBag.DailyTrend = dailyTrend;

            return View();
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
