using ExpenseManagementSystem.ExpenseManagement.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Interface
{
    public interface IExpenseRepository
    {

        Task<IEnumerable<Expense>> GetByGroupIdAsync(string groupId);
        Task<Expense> GetByIdAsync(string expenseId);
        Task UpdateAsync(Expense expense);
        Task<decimal> GetTotalAmountOwnedByUserAsync(string userId, string groupId);

        Task AddAsync(Expense expense);
        //Task<IEnumerable<Expense>> GetByGroupIdAsync(string groupId);
        //Task<Expense> GetByIdAsync(string expenseId);
        //Task UpdateAsync(Expense expense);
    }
}
