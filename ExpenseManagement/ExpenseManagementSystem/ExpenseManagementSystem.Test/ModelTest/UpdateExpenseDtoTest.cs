using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class UpdateExpenseDtoTest
    {
        [Fact]
        public void Should_SetAndGet_Id()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedId = "exp123";

            // Act
            updateExpenseDto.Id = expectedId;
            var actualId = updateExpenseDto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void Should_SetAndGet_Amount()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedAmount = 123.45m;

            // Act
            updateExpenseDto.Amount = expectedAmount;
            var actualAmount = updateExpenseDto.Amount;

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void Should_SetAndGet_Date()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedDate = new DateTime(2024, 7, 8);

            // Act
            updateExpenseDto.Date = expectedDate;
            var actualDate = updateExpenseDto.Date;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void Should_SetAndGet_Description()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedDescription = "Dinner with friends";

            // Act
            updateExpenseDto.Description = expectedDescription;
            var actualDescription = updateExpenseDto.Description;

            // Assert
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        public void Should_SetAndGet_GroupId()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedGroupId = "grp456";

            // Act
            updateExpenseDto.GroupId = expectedGroupId;
            var actualGroupId = updateExpenseDto.GroupId;

            // Assert
            Assert.Equal(expectedGroupId, actualGroupId);
        }

        [Fact]
        public void Should_SetAndGet_PaidById()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedPaidById = "user789";

            // Act
            updateExpenseDto.PaidById = expectedPaidById;
            var actualPaidById = updateExpenseDto.PaidById;

            // Assert
            Assert.Equal(expectedPaidById, actualPaidById);
        }

        [Fact]
        public void Should_Allow_Null_Id()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();

            // Act
            updateExpenseDto.Id = null;
            var actualId = updateExpenseDto.Id;

            // Assert
            Assert.Null(actualId);
        }

        [Fact]
        public void Should_Allow_Negative_Amount()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();
            var expectedAmount = -123.45m;

            // Act
            updateExpenseDto.Amount = expectedAmount;
            var actualAmount = updateExpenseDto.Amount;

            // Assert
            Assert.Equal(expectedAmount, actualAmount);
        }

        [Fact]
        public void Should_Allow_Null_Description()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();

            // Act
            updateExpenseDto.Description = null;
            var actualDescription = updateExpenseDto.Description;

            // Assert
            Assert.Null(actualDescription);
        }

        [Fact]
        public void Should_Allow_Null_GroupId()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();

            // Act
            updateExpenseDto.GroupId = null;
            var actualGroupId = updateExpenseDto.GroupId;

            // Assert
            Assert.Null(actualGroupId);
        }

        [Fact]
        public void Should_Allow_Null_PaidById()
        {
            // Arrange
            var updateExpenseDto = new UpdateExpenseDto();

            // Act
            updateExpenseDto.PaidById = null;
            var actualPaidById = updateExpenseDto.PaidById;

            // Assert
            Assert.Null(actualPaidById);
        }
    }
}
