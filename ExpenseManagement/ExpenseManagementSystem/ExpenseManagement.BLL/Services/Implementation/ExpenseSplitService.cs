using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;

public class ExpenseSplitService : IExpenseSplitService
{
    private readonly IExpenseSplitRepository _expenseSplitRepository;

    public ExpenseSplitService(IExpenseSplitRepository expenseSplitRepository)
    {
        _expenseSplitRepository = expenseSplitRepository;
    }

    public async Task<List<ExpenseSplit>> GetAllExpenseSplitsAsync()
    {
        return await _expenseSplitRepository.GetAllExpenseSplitsAsync();
    }
}
