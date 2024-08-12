using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Implementation
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseSharingContext _context;

        public ExpenseRepository(ExpenseSharingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetByGroupIdAsync(string groupId)
        {
            return await _context.Expenses
                .Include(e => e.ExpenseSplits)
                .Include(e => e.PaidBy)
                .Where(e => e.GroupId == groupId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Expense> GetByIdAsync(string expenseId)
        {
            return await _context.Expenses
                .Include(e => e.ExpenseSplits)
                .Include(e => e.PaidBy)
                .FirstOrDefaultAsync(e => e.Id == expenseId);
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
           
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalAmountOwnedByUserAsync(string userId, string groupId)
        {
            return await _context.ExpenseSplits
             .Where(es => es.UserId == userId && es.Expense.GroupId == groupId)
             .SumAsync(es => es.Owe);
            //return await _context.Expenses
            //    .Where(e => e.PaidBy.Id == userId && e.GroupId == groupId)
            //    .SumAsync(e => e.Amount);
        }
        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }
    }
}

