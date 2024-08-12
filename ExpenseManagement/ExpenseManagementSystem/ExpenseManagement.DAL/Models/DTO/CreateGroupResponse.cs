using ExpenseManagementSystem.ExpenseManagement.DAL;
using System;
using System.Collections.Generic;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class CreateGroupResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<MemberDto> Members { get; set; }
        public List<ExpenseDto> Expenses { get; set; }
        public List<string> MemberIds { get; set; }
       
    }
}

