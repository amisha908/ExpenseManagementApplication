using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.RepositoryTest
{
    public class ExpenseRepositoryTests
    {
        private readonly DbContextOptions<ExpenseSharingContext> _options;
        private readonly ExpenseSharingContext _context;
        private readonly ExpenseRepository _repository;

        public ExpenseRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ExpenseSharingContext(_options);
            _repository = new ExpenseRepository(_context);
        }

        [Fact]
        public async Task GetByGroupIdAsync_ShouldReturnExpenses_WhenGroupIdExists()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new Expense { Id = "1", Description = "Expense 1", GroupId = "group1", ExpenseSplits = new List<ExpenseSplit>(), PaidBy = new ApplicationUser { Name = "John Doe" } },
                new Expense { Id = "2", Description = "Expense 2", GroupId = "group1", ExpenseSplits = new List<ExpenseSplit>(), PaidBy = new ApplicationUser { Name = "Jane Doe" } }
            };

            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByGroupIdAsync("group1");

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByGroupIdAsync_ShouldReturnEmpty_WhenGroupIdDoesNotExist()
        {
            // Act
            var result = await _repository.GetByGroupIdAsync("nonexistentGroup");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnExpense_WhenExpenseIdExists()
        {
            // Arrange
            var expense = new Expense
            {
                Id = "1",
                Description = "Test Expense",
                GroupId = "group1", // Set the GroupId here
                ExpenseSplits = new List<ExpenseSplit>(),
                PaidBy = new ApplicationUser { Name = "John Doe" }
            };
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetByIdAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1", result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenExpenseIdDoesNotExist()
        {
            // Act
            var result = await _repository.GetByIdAsync("nonexistentId");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateExpense()
        {
            // Arrange
            var expense = new Expense { Id = "1", Description = "Test Expense", GroupId = "group1", PaidById = "user1" };
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            // Act
            expense.Amount = 200;
            await _repository.UpdateAsync(expense);
            var updatedExpense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == "1");

            // Assert
            Assert.Equal(200, updatedExpense.Amount);
        }


        [Fact]
        public async Task GetTotalAmountOwnedByUserAsync_ShouldReturnTotalAmount()
        {
            // Arrange
            var expenses = new List<Expense>
    {
        new Expense { Id = "1", Description = "Expense 1", GroupId = "group1", ExpenseSplits = new List<ExpenseSplit>(), PaidBy = new ApplicationUser { Name = "John Doe" } }
    };

            var expenseSplits = new List<ExpenseSplit>
    {
        new ExpenseSplit { Id = "1", UserId = "user1", Owe = 10, ExpenseId = "1" },
        new ExpenseSplit { Id = "2", UserId = "user1", Owe = 20, ExpenseId = "1" }
    };

            _context.Expenses.AddRange(expenses);
            _context.ExpenseSplits.AddRange(expenseSplits);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTotalAmountOwnedByUserAsync("user1", "group1");

            // Assert
            Assert.Equal(30, result);
        }


        [Fact]
        public async Task GetTotalAmountOwnedByUserAsync_ShouldReturnZero_WhenNoExpensesFound()
        {
            // Act
            var result = await _repository.GetTotalAmountOwnedByUserAsync("user1", "group1");

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddExpense()
        {
            // Arrange
            var expense = new Expense
            {
                Id = "1",
                Description = "Test Expense",
                GroupId = "group1",
                PaidById = "user1",
                PaidBy = new ApplicationUser { Id = "user1", Name = "John Doe" },
                Amount = 100,
                ExpenseSplits = new List<ExpenseSplit>()
            };

            // Act
            await _repository.AddAsync(expense);
            var addedExpense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == "1");

            // Assert
            Assert.NotNull(addedExpense);
            Assert.Equal("1", addedExpense.Id);
        }
    }
}
