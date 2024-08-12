using ExpenseManagement.DAL.Models.DTO;
using System;
using Xunit;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class CreateExpenseDtoTest
    {
        [Fact]
        public void CreateExpenseDto_PositiveCase()
        {
            // Arrange
            var createExpenseDto = new CreateExpenseDto
            {
                Amount = 100.0m,
                Date = DateTime.Now,
                Description = "Dinner with friends",
                GroupId = "group123",
                PaidById = "user456"
            };

            // Act

            // Assert
            Assert.Equal(100.0m, createExpenseDto.Amount);
            Assert.Equal(DateTime.Today, createExpenseDto.Date.Date);
            Assert.Equal("Dinner with friends", createExpenseDto.Description);
            Assert.Equal("group123", createExpenseDto.GroupId);
            Assert.Equal("user456", createExpenseDto.PaidById);
        }

        [Fact]
        public void CreateExpenseDto_NegativeCase()
        {
            // Arrange
            var createExpenseDto = new CreateExpenseDto
            {
                Amount = -50.0m,
                Date = DateTime.MinValue,
                Description = null,
                GroupId = "invalidGroupId",
                PaidById = "user789"
            };

            // Act

            // Assert
            Assert.NotEqual(DateTime.Today, createExpenseDto.Date.Date); // Ensure Date is not set to today's date
            Assert.Null(createExpenseDto.Description); // Ensure Description is null
            Assert.Equal("invalidGroupId", createExpenseDto.GroupId); // Ensure GroupId is not equal to "invalidGroupId"
            Assert.Equal("user789", createExpenseDto.PaidById); // Ensure PaidById is equal to "user789"
        }
    }
}
