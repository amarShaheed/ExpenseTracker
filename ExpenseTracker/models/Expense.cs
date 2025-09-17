namespace ExpenseTracker.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Notes { get; set; }

        public Category? Category { get; set; }

    }
}
