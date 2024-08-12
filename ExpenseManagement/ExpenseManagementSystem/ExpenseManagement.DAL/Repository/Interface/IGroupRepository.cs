using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Interface
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(string id);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<Group> AddGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Group group);
        Task DeleteGroupAsync(string id);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<bool> GroupExistsAsync(string name);
        Task DeleteRelatedExpensesAndSplitsAsync(string id);
       
    }
}
