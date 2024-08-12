using ExpenseManagementSystem.ExpenseManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Interface
{
    public interface IExpenseSplitService
    {
        Task<List<ExpenseSplit>> GetAllExpenseSplitsAsync();
    }
}
