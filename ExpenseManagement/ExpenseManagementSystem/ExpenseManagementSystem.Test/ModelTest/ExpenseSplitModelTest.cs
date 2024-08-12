using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class ExpenseSplitModelTest
    {
        [Fact]
        public void ExpenseSplit_CanSetAndGetId()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var id = "test-id";

            // Act
            expenseSplit.Id = id;

            // Assert
            Assert.Equal(id, expenseSplit.Id);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetExpenseId()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var expenseId = "expense-id";

            // Act
            expenseSplit.ExpenseId = expenseId;

            // Assert
            Assert.Equal(expenseId, expenseSplit.ExpenseId);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetUserId()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var userId = "user-id";

            // Act
            expenseSplit.UserId = userId;

            // Assert
            Assert.Equal(userId, expenseSplit.UserId);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetAmount()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var amount = 100.50m;

            // Act
            expenseSplit.Amount = amount;

            // Assert
            Assert.Equal(amount, expenseSplit.Amount);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetOwe()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var owe = 50.25m;

            // Act
            expenseSplit.Owe = owe;

            // Assert
            Assert.Equal(owe, expenseSplit.Owe);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetOwn()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var own = 50.25m;

            // Act
            expenseSplit.Own = own;

            // Assert
            Assert.Equal(own, expenseSplit.Own);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetExpense()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var expense = new Expense { Id = "expense-id" };

            // Act
            expenseSplit.Expense = expense;

            // Assert
            Assert.Equal(expense, expenseSplit.Expense);
        }

        [Fact]
        public void ExpenseSplit_CanSetAndGetUser()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();
            var user = new ApplicationUser { Id = "user-id" };

            // Act
            expenseSplit.User = user;

            // Assert
            Assert.Equal(user, expenseSplit.User);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForNullId()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Null(expenseSplit.Id);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForNullExpenseId()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Null(expenseSplit.ExpenseId);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForNullUserId()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Null(expenseSplit.UserId);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForDefaultAmount()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Equal(0, expenseSplit.Amount);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForDefaultOwe()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Equal(0, expenseSplit.Owe);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForDefaultOwn()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Equal(0, expenseSplit.Own);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForNullExpense()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Null(expenseSplit.Expense);
        }

        [Fact]
        public void ExpenseSplit_Negative_TestForNullUser()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit();

            // Act & Assert
            Assert.Null(expenseSplit.User);
        }
    }
}
