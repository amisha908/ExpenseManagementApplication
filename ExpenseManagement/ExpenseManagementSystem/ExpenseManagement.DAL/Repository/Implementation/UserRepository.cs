using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Implementation
{
    public class UserRepository:IUserRepository
    {
        private readonly ExpenseSharingContext _expenseSharingDbContext;

        public UserRepository(ExpenseSharingContext expenseSharingDbContext)
        {
            _expenseSharingDbContext = expenseSharingDbContext;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _expenseSharingDbContext.Users.ToListAsync();
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _expenseSharingDbContext.Users.FindAsync(id);
        }
    }
}
