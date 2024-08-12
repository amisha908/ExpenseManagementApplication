using System;
using System.Collections.Generic;

namespace ExpenseManagementSystem.ExpenseManagement.DAL
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
        public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}

