using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
