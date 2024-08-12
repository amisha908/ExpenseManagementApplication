using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Interface
{
    public interface IMemberService
    {
        Task<IEnumerable<GetAllGroupDto>> GetGroupsByUserIdAsync(string userId);
        Task<IEnumerable<MemberDto>> GetMemberByGroupId(string groupId);
    }
}
