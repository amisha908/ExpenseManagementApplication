using AutoMapper;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Implementation
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ExpenseSharingContext _context;
        private readonly IMapper _mapper;

        public GroupRepository(ExpenseSharingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExpenseManagementSystem.ExpenseManagement.DAL.Group> GetGroupByIdAsync(string id)
        {
            return await _context.Groups
                          .Include(g => g.Members)
                          .ThenInclude(m => m.User)  // Include the User of each Member
                          .Include(g => g.Expenses)
                          .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<ExpenseManagementSystem.ExpenseManagement.DAL.Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }
       
        public async Task<ExpenseManagementSystem.ExpenseManagement.DAL.Group> AddGroupAsync(ExpenseManagementSystem.ExpenseManagement.DAL.Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<ExpenseManagementSystem.ExpenseManagement.DAL.Group> UpdateGroupAsync(ExpenseManagementSystem.ExpenseManagement.DAL.Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task DeleteGroupAsync(string id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> GroupExistsAsync(string name)
        {
            return await _context.Groups.AnyAsync(g => g.Name == name);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            //return await _context.Groups
            //    .Include(g => g.Members)  // Include the Members of the group
            //        .ThenInclude(m => m.User)  // Then include the User of each Member
            //    .Include(g => g.Expenses)  // Include the Expenses related to the group
            //    .FirstOrDefaultAsync(g => g.Id == id);

            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task DeleteRelatedExpensesAndSplitsAsync(string groupId)
        {
            var expenses = await _context.Expenses
                                 .Where(e => e.GroupId == groupId)
                                 .ToListAsync();

            foreach (var expense in expenses)
            {
                var expenseSplits = await _context.ExpenseSplits
                                                  .Where(es => es.ExpenseId == expense.Id)
                                                  .ToListAsync();

                _context.ExpenseSplits.RemoveRange(expenseSplits);
            }

            _context.Expenses.RemoveRange(expenses);
            await _context.SaveChangesAsync();
        }
    }
}


