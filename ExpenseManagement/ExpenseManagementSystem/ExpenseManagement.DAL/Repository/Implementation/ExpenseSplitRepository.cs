using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Implementation
{
    public class ExpenseSplitRepository : IExpenseSplitRepository
    {
        private readonly ExpenseSharingContext _context;

        public ExpenseSplitRepository(ExpenseSharingContext context)
        {
            _context = context;
        }

        public async Task AddExpenseSplitAsync(ExpenseSplit expenseSplit)
        {
            await _context.ExpenseSplits.AddAsync(expenseSplit);
            await _context.SaveChangesAsync();
        }

        public async Task<ExpenseSplit> GetSplitByExpenseIdAndUserIdAsync(string expenseId, string userId)
        {
            return await _context.ExpenseSplits
                .FirstOrDefaultAsync(es => es.ExpenseId == expenseId && es.UserId == userId);
        }

        public async Task<List<ExpenseSplit>> GetAllExpenseSplitsAsync()
        {
            return await _context.ExpenseSplits
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<ExpenseSplit>> GetSplitsByExpenseIdAsync(string id)
        {
            return await _context.ExpenseSplits
                .Where(es => es.ExpenseId == id)
                .Distinct()
                .ToListAsync();
        }

        public async Task<decimal> GetTotalAmountOwedByUserAsync(string userId)
        {
            // Calculate the total amount owed by the user across all splits
            var totalOwed = await _context.ExpenseSplits
                 .Where(es => es.UserId == userId)
                 .SumAsync(es => es.Owe);

            return totalOwed;
        }
        public async Task<IEnumerable<ExpenseSplit>> GetExpenseSplitsByUserIdAsync(string userId)
        {
            return await _context.ExpenseSplits.Where(es => es.UserId == userId).ToListAsync();
        }
        public async Task UpdateExpenseSplitAsync(ExpenseSplit expenseSplit)
        {
            _context.ExpenseSplits.Update(expenseSplit);
            await _context.SaveChangesAsync();
        }

    }
}

