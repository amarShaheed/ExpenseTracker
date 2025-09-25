using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            // Total Expenses
            var totalExpenses = await _context.Expenses
                .Where(e => e.ExpenseDate >= startOfMonth)
                .SumAsync(e => (decimal?)e.Amount) ?? 0;

            // Category totals
            var categoryTotals = await _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => e.Category.Name)
                .Select(g => new CategoryTotal
                {
                    Category = g.Key,
                    Total = g.Sum(e => e.Amount)
                })
                .ToListAsync() ?? new List<CategoryTotal>();

            // Daily trend totals
            var dailyTrend = await _context.Expenses
                .GroupBy(e => e.ExpenseDate.Date)
                .Select(g => new DailyTotal
                {
                    Date = g.Key,
                    Total = g.Sum(e => e.Amount)
                })
                .OrderBy(d => d.Date)
                .ToListAsync() ?? new List<DailyTotal>();

            var model = new DashboardViewModel
            {
                TotalExpenses = totalExpenses,
                CategoryTotals = categoryTotals,
                DailyTrend = dailyTrend
            };

            return View(model);
        }

        public class CategoryTotal
        {
            public string Category { get; set; } = string.Empty;
            public decimal Total { get; set; }
        }

        public class DailyTotal
        {
            public DateTime Date { get; set; }
            public decimal Total { get; set; }
        }

        public class DashboardViewModel
        {
            public decimal TotalExpenses { get; set; }
            public List<CategoryTotal> CategoryTotals { get; set; } = new();
            public List<DailyTotal> DailyTrend { get; set; } = new();
        }
    }
}
