using ExpenseManagementSystem.Models;
using ExpenseManagementSystem.Models.Domain;

namespace ExpenseManagementSystem.ExpenseManagement.DAL
{
    public class ExpenseSplit
    {
        public string Id { get; set; }
        public string ExpenseId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public decimal Owe { get; set; }
        public decimal Own { get; set; }

        public Expense Expense { get; set; }
       
        public ApplicationUser User { get; set; }
    }
}
