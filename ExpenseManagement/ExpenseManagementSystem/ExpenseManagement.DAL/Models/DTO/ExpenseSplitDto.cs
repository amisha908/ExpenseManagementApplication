using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class ExpenseSplitDto
    {
        public string Id { get; set; }
        public string ExpenseId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public decimal Owe { get; set; }
        public decimal Own { get; set; }
    }
}
