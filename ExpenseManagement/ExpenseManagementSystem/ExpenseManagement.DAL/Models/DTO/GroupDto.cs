
using System;
using System.Collections.Generic;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class GroupDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<MemberDto> Members { get; set; }
        public ICollection<ExpenseDto> Expenses { get; set; }
       
    }
}
