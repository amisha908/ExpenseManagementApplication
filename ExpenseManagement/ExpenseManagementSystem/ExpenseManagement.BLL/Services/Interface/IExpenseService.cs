using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Interface
{
    public interface IExpenseService
    {
        Task<ExpenseDto> AddExpenseAsync(CreateExpenseDto createExpenseDto);
        Task AddExpenseSplitsAsync(string groupId);
        Task<IEnumerable<ExpenseDto>> GetExpensesByGroupIdAsync(string groupId);
        Task SettleExpensesAsync(List<string> expenseIds);
    }
}
