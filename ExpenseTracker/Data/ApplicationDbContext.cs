//using Microsoft.EntityFrameworkCore;
//using ExpenseTracker.models;  // <-- your models namespace

//namespace ExpenseTracker.Data
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Expense> Expenses { get; set; }
//    }
//}
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;  // adjust if namespace is different


namespace ExpenseTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Expense> Expenses { get; set; }


        // 🔽 Add this method here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default categories
            //modelBuilder.Entity<Category>().HasData(
                modelBuilder.Entity<Category>().HasData(
               new Category { CategoryId = 1, Name = "Food" },
               new Category { CategoryId = 2, Name = "Transport" },
               new Category { CategoryId = 3, Name = "Shopping" }
                );

            
        }
    }
}
