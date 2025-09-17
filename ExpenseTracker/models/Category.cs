namespace ExpenseTracker.Models  // ✅ Uppercase 'Models'
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Optional: navigation property for related expenses
        //public ICollection<Expense> Expenses { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    }
}
