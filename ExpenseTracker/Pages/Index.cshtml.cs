using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) => _context = context;

        public decimal TotalThisMonth { get; set; }
        public Dictionary<string, decimal> CategorySummary { get; set; } = new();
        public List<string> MonthlyLabels { get; set; } = new();
        public List<decimal> MonthlyTotals { get; set; } = new();

        public async Task OnGetAsync()
        {
            var now = DateTime.Now;

            TotalThisMonth = await _context.Expenses
                .Where(e => e.ExpenseDate.Month == now.Month && e.ExpenseDate.Year == now.Year)
                .SumAsync(e => (decimal?)e.Amount) ?? 0m;

            CategorySummary = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.ExpenseDate.Month == now.Month && e.ExpenseDate.Year == now.Year)
                .GroupBy(e => e.Category.Name)
                .ToDictionaryAsync(g => g.Key ?? "Uncategorized", g => g.Sum(e => e.Amount));

            // Monthly trends (last 6 months)
            var sixMonthsAgo = now.AddMonths(-5);
            var monthly = await _context.Expenses
                .Where(e => e.ExpenseDate >= new DateTime(sixMonthsAgo.Year, sixMonthsAgo.Month, 1))
                .GroupBy(e => new { e.ExpenseDate.Year, e.ExpenseDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(e => e.Amount)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();

            // build labels and totals (ensure months with 0 appear)
            var months = Enumerable.Range(0, 6)
                .Select(i => new DateTime(now.Year, now.Month, 1).AddMonths(-i))
                .Reverse()
                .ToList();

            foreach (var dt in months)
            {
                var entry = monthly.FirstOrDefault(m => m.Year == dt.Year && m.Month == dt.Month);
                MonthlyLabels.Add(dt.ToString("MMM yyyy", CultureInfo.InvariantCulture));
                MonthlyTotals.Add(entry?.Total ?? 0m);
            }
        }
    }
}
