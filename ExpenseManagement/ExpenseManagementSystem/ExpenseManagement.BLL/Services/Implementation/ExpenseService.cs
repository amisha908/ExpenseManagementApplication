using AutoMapper;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Implementation
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseSplitRepository _expenseSplitRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public ExpenseService(IExpenseRepository expenseRepository, IExpenseSplitRepository expenseSplitRepository, IGroupRepository groupRepository, IMapper mapper, IUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _expenseSplitRepository = expenseSplitRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ExpenseDto> AddExpenseAsync(CreateExpenseDto createExpenseDto)
        {
            var group = await _groupRepository.GetGroupByIdAsync(createExpenseDto.GroupId);
            if (group == null)
            {
                throw new ArgumentException("Group not found");
            }

            var expense = _mapper.Map<Expense>(createExpenseDto);
            expense.Id = Guid.NewGuid().ToString();
            await _expenseRepository.AddAsync(expense);

            return _mapper.Map<ExpenseDto>(expense);
            //var expense = _mapper.Map<Expense>(createExpenseDto);
            //expense.Id = Guid.NewGuid().ToString();
            //await _expenseRepository.AddAsync(expense);

            //return _mapper.Map<ExpenseDto>(expense);
        }

        public async Task AddExpenseSplitsAsync(string groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                throw new ArgumentException("Group not found");
            }

            var members = group.Members;
            if (members == null || !members.Any())
            {
                throw new InvalidOperationException("No members found in the group");
            }

            var expenses = await _expenseRepository.GetByGroupIdAsync(groupId);
            foreach (var expense in expenses)
            {
                decimal individualSplitAmount = expense.Amount / members.Count();

                foreach (var member in members)
                {
                    var existingSplit = await _expenseSplitRepository.GetSplitByExpenseIdAndUserIdAsync(expense.Id, member.UserId);
                    if (existingSplit == null)
                    {
                        var expenseSplit = new ExpenseSplit
                        {
                            Id = Guid.NewGuid().ToString(),
                            ExpenseId = expense.Id,
                            UserId = member.UserId,
                            
                            Amount = individualSplitAmount
                        };

                        // Adjust the own and owe amounts
                        if (member.UserId == expense.PaidById)
                        {
                            expenseSplit.Own = expense.Amount - individualSplitAmount;
                            expenseSplit.Owe = 0;
                        }
                        else
                        {
                            expenseSplit.Own = 0;
                            expenseSplit.Owe = individualSplitAmount;
                        }

                        await _expenseSplitRepository.AddExpenseSplitAsync(expenseSplit);
                    }
                }
            }
        }


        public async Task<IEnumerable<ExpenseDto>> GetExpensesByGroupIdAsync(string groupId)
        {
            var expenses = await _expenseRepository.GetByGroupIdAsync(groupId);
            Console.WriteLine($"Expenses count from repository: {expenses?.Count() ?? 0}");

            var distinctExpenses = expenses?.GroupBy(e => e.Id).Select(g => g.First()).ToList() ?? new List<Expense>();
            Console.WriteLine($"Distinct expenses count: {distinctExpenses.Count}");

            var expenseDtos = _mapper.Map<IEnumerable<ExpenseDto>>(distinctExpenses);
            Console.WriteLine($"ExpenseDtos count after mapping: {expenseDtos?.Count() ?? 0}");

            foreach (var expenseDto in expenseDtos)
            {
                var splits = await _expenseSplitRepository.GetSplitsByExpenseIdAsync(expenseDto.Id);
                Console.WriteLine($"Splits count for expense {expenseDto.Id}: {splits?.Count() ?? 0}");

                var distinctSplits = splits?.GroupBy(s => new { s.UserId, s.ExpenseId })
                                            .Select(g => g.First())
                                            .ToList() ?? new List<ExpenseSplit>();
                Console.WriteLine($"Distinct splits count: {distinctSplits.Count}");

                expenseDto.ExpenseSplits = _mapper.Map<List<ExpenseSplitDto>>(distinctSplits);
                Console.WriteLine($"ExpenseSplits count after mapping: {expenseDto.ExpenseSplits?.Count ?? 0}");

                // Calculate owns and owes for each user
                foreach (var split in expenseDto.ExpenseSplits)
                {
                    var user = await _userRepository.GetUserByIdAsync(split.UserId);
                    split.UserName = user?.Name ?? "Unknown";
                    if (split.UserId == expenseDto.PaidById)
                    {
                        split.Own = expenseDto.Amount - split.Amount;
                        split.Owe = 0;
                    }
                    else
                    {
                        split.Own = 0;
                        split.Owe = split.Amount;
                    }
                }
            }

            Console.WriteLine($"Final ExpenseDtos count: {expenseDtos?.Count() ?? 0}");
            return expenseDtos;
        }
        public void CalculateSplitOwnership(ExpenseDto expenseDto, ExpenseSplitDto split)
        {
            // Calculate ownership based on who paid the expense
            if (split.UserId == expenseDto.PaidById)
            {
                split.Own = expenseDto.Amount - split.Amount;
                split.Owe = 0;
            }
            else
            {
                split.Own = 0;
                split.Owe = split.Amount;
            }
        }




        public async Task<decimal> CalculateOweAmount(string userId, string expenseId)
        {
            var split = await _expenseSplitRepository.GetSplitByExpenseIdAndUserIdAsync(expenseId, userId);
            if (split != null)
            {
                // Calculate how much the user owes
                return split.Owe; // Owe amount directly from the split
            }
            return 0;
        }

        public async Task SettleExpensesAsync(List<string> expenseIds)
        {
            foreach (var expenseId in expenseIds)
            {
                var expense = await _expenseRepository.GetByIdAsync(expenseId);
                if (expense == null)
                {
                    // Log the missing expense or skip to the next expenseId
                    continue;
                }

                expense.IsSettled = true;
                await _expenseRepository.UpdateAsync(expense);

                var expenseSplits = await _expenseSplitRepository.GetSplitsByExpenseIdAsync(expenseId);
                if (expenseSplits != null)
                {
                    foreach (var split in expenseSplits)
                    {
                        split.Owe = 0;
                        split.Own = 0;
                        await _expenseSplitRepository.UpdateExpenseSplitAsync(split);
                    }
                }
            }
        }

    }
}

