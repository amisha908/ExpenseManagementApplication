using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class CreateExpenseDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string GroupId { get; set; }
        public string PaidById { get; set; }
        }
}
