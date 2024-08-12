using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class CreateExpenseSplitDto
    {
        public string GroupId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
