using Microsoft.AspNetCore.Identity;

namespace ExpenseManagementSystem.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; } = "User"; // Default role
    }
}
