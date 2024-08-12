using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class ExpenseDtoTest
    {
        [Fact]
        public void Should_SetAndGet_Id()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedId = "expense123";

            // Act
            expenseDto.Id = expectedId;
            var actualId = expenseDto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void Should_SetAndGet_Amount()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedAmount = 100m;

            // Act
            expenseDto.Amount = expectedAmount;
            var actualAmount = expenseDto.Amount;

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void Should_SetAndGet_Date()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedDate = new DateTime(2023, 7, 1);

            // Act
            expenseDto.Date = expectedDate;
            var actualDate = expenseDto.Date;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void Should_SetAndGet_Description()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedDescription = "Dinner expense";

            // Act
            expenseDto.Description = expectedDescription;
            var actualDescription = expenseDto.Description;

            // Assert
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        public void Should_SetAndGet_GroupId()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedGroupId = "group123";

            // Act
            expenseDto.GroupId = expectedGroupId;
            var actualGroupId = expenseDto.GroupId;

            // Assert
            Assert.Equal(expectedGroupId, actualGroupId);
        }

        [Fact]
        public void Should_SetAndGet_PaidById()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedPaidById = "user123";

            // Act
            expenseDto.PaidById = expectedPaidById;
            var actualPaidById = expenseDto.PaidById;

            // Assert
            Assert.Equal(expectedPaidById, actualPaidById);
        }

        [Fact]
        public void Should_SetAndGet_Name()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedName = "Lunch";

            // Act
            expenseDto.Name = expectedName;
            var actualName = expenseDto.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Should_SetAndGet_IsSettled()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedIsSettled = "true";

            // Act
            expenseDto.IsSettled = expectedIsSettled;
            var actualIsSettled = expenseDto.IsSettled;

            // Assert
            Assert.Equal(expectedIsSettled, actualIsSettled);
        }

        [Fact]
        public void Should_SetAndGet_ExpenseSplits()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedSplits = new List<ExpenseSplitDto>
            {
                new ExpenseSplitDto { Id = "split1" },
                new ExpenseSplitDto { Id = "split2" }
            };

            // Act
            expenseDto.ExpenseSplits = expectedSplits;
            var actualSplits = expenseDto.ExpenseSplits;

            // Assert
            Assert.Equal(expectedSplits, actualSplits);
        }

        [Fact]
        public void Should_Allow_Null_Id()
        {
            // Arrange
            var expenseDto = new ExpenseDto();

            // Act
            expenseDto.Id = null;
            var actualId = expenseDto.Id;

            // Assert
            Assert.Null(actualId);
        }

        [Fact]
        public void Should_Allow_Null_Description()
        {
            // Arrange
            var expenseDto = new ExpenseDto();

            // Act
            expenseDto.Description = null;
            var actualDescription = expenseDto.Description;

            // Assert
            Assert.Null(actualDescription);
        }

        [Fact]
        public void Should_Allow_Null_GroupId()
        {
            // Arrange
            var expenseDto = new ExpenseDto();

            // Act
            expenseDto.GroupId = null;
            var actualGroupId = expenseDto.GroupId;

            // Assert
            Assert.Null(actualGroupId);
        }

        [Fact]
        public void Should_Allow_Null_PaidById()
        {
            // Arrange
            var expenseDto = new ExpenseDto();

            // Act
            expenseDto.PaidById = null;
            var actualPaidById = expenseDto.PaidById;

            // Assert
            Assert.Null(actualPaidById);
        }

        [Fact]
        public void Should_Allow_Null_Name()
        {
            // Arrange
            var expenseDto = new ExpenseDto();

            // Act
            expenseDto.Name = null;
            var actualName = expenseDto.Name;

            // Assert
            Assert.Null(actualName);
        }

        [Fact]
        public void Should_Allow_Null_IsSettled()
        {
            // Arrange
            var expenseDto = new ExpenseDto();

            // Act
            expenseDto.IsSettled = null;
            var actualIsSettled = expenseDto.IsSettled;

            // Assert
            Assert.Null(actualIsSettled);
        }

        [Fact]
        public void Should_Allow_Negative_Amount()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedAmount = -100m;

            // Act
            expenseDto.Amount = expectedAmount;
            var actualAmount = expenseDto.Amount;

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void Should_Allow_Invalid_Date()
        {
            // Arrange
            var expenseDto = new ExpenseDto();
            var expectedDate = DateTime.MinValue;

            // Act
            expenseDto.Date = expectedDate;
            var actualDate = expenseDto.Date;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }
    }
}
