using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class UserBalance
    {
        public string UserId { get; set; }
        public decimal Owns { get; set; }
        public decimal Owes { get; set; }
    }
}
