using ExpenseManagementSystem.Models.Domain;

namespace ExpenseManagementSystem.ExpenseManagement.DAL
{
    public class Member
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}

