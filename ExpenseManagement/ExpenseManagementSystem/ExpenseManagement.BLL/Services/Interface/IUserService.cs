using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<decimal> GetUserTotalOwesAsync(string userId);
        Task<decimal> GetUserTotalOwnsAsync(string userId);
    }
}
