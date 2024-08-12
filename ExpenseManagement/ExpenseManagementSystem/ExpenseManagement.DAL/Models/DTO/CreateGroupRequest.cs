using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class CreateGroupRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public List<string> MemberIds { get; set; }

    }
}
