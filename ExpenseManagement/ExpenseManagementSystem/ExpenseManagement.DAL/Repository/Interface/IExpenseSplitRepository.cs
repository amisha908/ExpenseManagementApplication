using ExpenseManagementSystem.ExpenseManagement.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Interface
{
    public interface IExpenseSplitRepository
    {
        Task AddExpenseSplitAsync(ExpenseSplit expenseSplit);
        Task<ExpenseSplit> GetSplitByExpenseIdAndUserIdAsync(string expenseId, string userId);
        Task<List<ExpenseSplit>> GetAllExpenseSplitsAsync();
        Task<List<ExpenseSplit>> GetSplitsByExpenseIdAsync(string id);
        public Task<decimal> GetTotalAmountOwedByUserAsync(string userId);
        Task<IEnumerable<ExpenseSplit>> GetExpenseSplitsByUserIdAsync(string userId);
        Task UpdateExpenseSplitAsync(ExpenseSplit expenseSplit);


    }
}
