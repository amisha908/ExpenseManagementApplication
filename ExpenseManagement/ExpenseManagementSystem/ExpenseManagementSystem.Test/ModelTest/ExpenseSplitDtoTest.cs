using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class ExpenseSplitDtoTest
    {
        [Fact]
        public void Should_SetAndGet_Id()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedId = "split123";

            // Act
            expenseSplitDto.Id = expectedId;
            var actualId = expenseSplitDto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void Should_SetAndGet_ExpenseId()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedExpenseId = "exp456";

            // Act
            expenseSplitDto.ExpenseId = expectedExpenseId;
            var actualExpenseId = expenseSplitDto.ExpenseId;

            // Assert
            Assert.Equal(expectedExpenseId, actualExpenseId);
        }

        [Fact]
        public void Should_SetAndGet_UserId()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedUserId = "user789";

            // Act
            expenseSplitDto.UserId = expectedUserId;
            var actualUserId = expenseSplitDto.UserId;

            // Assert
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public void Should_SetAndGet_UserName()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedUserName = "John Doe";

            // Act
            expenseSplitDto.UserName = expectedUserName;
            var actualUserName = expenseSplitDto.UserName;

            // Assert
            Assert.Equal(expectedUserName, actualUserName);
        }

        [Fact]
        public void Should_SetAndGet_Amount()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedAmount = 100m;

            // Act
            expenseSplitDto.Amount = expectedAmount;
            var actualAmount = expenseSplitDto.Amount;

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void Should_SetAndGet_Owe()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedOwe = 50m;

            // Act
            expenseSplitDto.Owe = expectedOwe;
            var actualOwe = expenseSplitDto.Owe;

            // Assert
            Assert.Equal(expectedOwe, actualOwe);
        }

        [Fact]
        public void Should_SetAndGet_Own()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedOwn = 50m;

            // Act
            expenseSplitDto.Own = expectedOwn;
            var actualOwn = expenseSplitDto.Own;

            // Assert
            Assert.Equal(expectedOwn, actualOwn);
        }

        [Fact]
        public void Should_Allow_Null_Id()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();

            // Act
            expenseSplitDto.Id = null;
            var actualId = expenseSplitDto.Id;

            // Assert
            Assert.Null(actualId);
        }

        [Fact]
        public void Should_Allow_Null_ExpenseId()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();

            // Act
            expenseSplitDto.ExpenseId = null;
            var actualExpenseId = expenseSplitDto.ExpenseId;

            // Assert
            Assert.Null(actualExpenseId);
        }

        [Fact]
        public void Should_Allow_Null_UserId()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();

            // Act
            expenseSplitDto.UserId = null;
            var actualUserId = expenseSplitDto.UserId;

            // Assert
            Assert.Null(actualUserId);
        }

        [Fact]
        public void Should_Allow_Null_UserName()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();

            // Act
            expenseSplitDto.UserName = null;
            var actualUserName = expenseSplitDto.UserName;

            // Assert
            Assert.Null(actualUserName);
        }

        [Fact]
        public void Should_Allow_Negative_Amount()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedAmount = -100m;

            // Act
            expenseSplitDto.Amount = expectedAmount;
            var actualAmount = expenseSplitDto.Amount;

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void Should_Allow_Negative_Owe()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedOwe = -50m;

            // Act
            expenseSplitDto.Owe = expectedOwe;
            var actualOwe = expenseSplitDto.Owe;

            // Assert
            Assert.Equal(expectedOwe, actualOwe);
        }

        [Fact]
        public void Should_Allow_Negative_Own()
        {
            // Arrange
            var expenseSplitDto = new ExpenseSplitDto();
            var expectedOwn = -50m;

            // Act
            expenseSplitDto.Own = expectedOwn;
            var actualOwn = expenseSplitDto.Own;

            // Assert
            Assert.Equal(expectedOwn, actualOwn);
        }
    }
}
