using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Models.DTO
{
    public class UpdateGroupRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [MinLength(1)]
        [MaxLength(10)]
        public List<string> MemberIds { get; set; } = new List<string>();
    }
}
