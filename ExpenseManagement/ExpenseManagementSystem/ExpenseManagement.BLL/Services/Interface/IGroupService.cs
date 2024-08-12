using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Interface
{
    public interface IGroupService
    {
        Task<CreateGroupResponse> CreateGroupAsync(CreateGroupRequest groupDto);
        Task<GroupDto> GetGroupByIdAsync(string id);
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync();
        Task<GroupDto> UpdateGroupAsync(UpdateGroupRequest updateGroupRequest);
        Task DeleteGroupAsync(string id);
    }
}
