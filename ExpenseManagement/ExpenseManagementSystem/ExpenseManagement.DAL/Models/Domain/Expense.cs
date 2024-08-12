using ExpenseManagementSystem.Models.Domain;
using System;
using System.Collections.Generic;

namespace ExpenseManagementSystem.ExpenseManagement.DAL
{
    public class Expense
    {
        public string Id { get; set; } 
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public virtual Group Group { get; set; }
        public string PaidById { get; set; }
        public virtual ApplicationUser PaidBy { get; set; }
        public bool IsSettled { get; set; }
        public virtual ICollection<ExpenseSplit> ExpenseSplits { get; set; } = new List<ExpenseSplit>();
        public Expense()
        {
            Id = Guid.NewGuid().ToString(); // Ensure Id is set
        }
    }
}


