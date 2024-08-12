using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
   
    public class ExpenseModelTest
    {
        [Fact]
        public void Expense_Initialization_DefaultValues()
        {
            // Arrange & Act
            var expense = new Expense();

            // Assert
            Assert.NotNull(expense.Id);
            Assert.NotEqual(Guid.Empty.ToString(), expense.Id);
            Assert.Equal(0, expense.Amount);
            Assert.Equal(default(DateTime), expense.Date);
            Assert.Null(expense.Description);
            Assert.Null(expense.GroupId);
            Assert.Null(expense.Group);
            Assert.Null(expense.PaidById);
            Assert.Null(expense.PaidBy);
            Assert.False(expense.IsSettled);
            Assert.NotNull(expense.ExpenseSplits);
            Assert.Empty(expense.ExpenseSplits);
        }

        [Fact]
        public void Expense_SetValidValues_ShouldSetCorrectly()
        {
            // Arrange
            var group = new Group { Id = "group1", Name = "Test Group" };
            var user = new ApplicationUser { Id = "user1", UserName = "Test User" };
            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "split1", Amount = 50 },
                new ExpenseSplit { Id = "split2", Amount = 50 }
            };
            var expense = new Expense
            {
                Amount = 100,
                Date = DateTime.Now,
                Description = "Test Description",
                GroupId = group.Id,
                Group = group,
                PaidById = user.Id,
                PaidBy = user,
                IsSettled = true,
                ExpenseSplits = expenseSplits
            };

            // Act & Assert
            Assert.Equal(100, expense.Amount);
            Assert.Equal("Test Description", expense.Description);
            Assert.Equal(group.Id, expense.GroupId);
            Assert.Equal(group, expense.Group);
            Assert.Equal(user.Id, expense.PaidById);
            Assert.Equal(user, expense.PaidBy);
            Assert.True(expense.IsSettled);
            Assert.Equal(2, expense.ExpenseSplits.Count);
        }

        [Fact]
        public void Expense_ExpenseSplits_ShouldBeEmptyInitially()
        {
            // Arrange
            var expense = new Expense();

            // Act & Assert
            Assert.NotNull(expense.ExpenseSplits);
            Assert.Empty(expense.ExpenseSplits);
        }

        [Fact]
        public void Expense_SetNullValues_ShouldHandleGracefully()
        {
            // Arrange
            var expense = new Expense();

            // Act
            expense.Description = null;
            expense.GroupId = null;
            expense.Group = null;
            expense.PaidById = null;
            expense.PaidBy = null;
            expense.ExpenseSplits = null;

            // Assert
            Assert.Null(expense.Description);
            Assert.Null(expense.GroupId);
            Assert.Null(expense.Group);
            Assert.Null(expense.PaidById);
            Assert.Null(expense.PaidBy);
            Assert.Null(expense.ExpenseSplits);
        }

        
        [Fact]
        public void Expense_SetNullId_ShouldGenerateNewId()
        {
            // Arrange
            var expense = new Expense { Id = null };

            // Act
            expense.Id = Guid.NewGuid().ToString();

            // Assert
            Assert.NotNull(expense.Id);
            Assert.NotEqual(Guid.Empty.ToString(), expense.Id);
        }

        [Fact]
        public void Expense_AddingExpenseSplits_ShouldWorkCorrectly()
        {
            // Arrange
            var expense = new Expense();
            var split = new ExpenseSplit { Id = "split1", Amount = 50 };

            // Act
            expense.ExpenseSplits.Add(split);

            // Assert
            Assert.Equal(1, expense.ExpenseSplits.Count);
            Assert.Equal(split, expense.ExpenseSplits.First());
        }

        [Fact]
        public void Expense_SetPastDate_ShouldWorkCorrectly()
        {
            // Arrange
            var expense = new Expense();
            var pastDate = new DateTime(2020, 1, 1);

            // Act
            expense.Date = pastDate;

            // Assert
            Assert.Equal(pastDate, expense.Date);
        }
    }
}
