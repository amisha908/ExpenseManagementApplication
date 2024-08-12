using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class CreateExpenseSplitDtoTest
    {
        [Fact]
        public void CreateExpenseSplitDto_SetAndGetProperties_Success()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var amount = 100.50m;

            // Act
            var createExpenseSplitDto = new CreateExpenseSplitDto
            {
                GroupId = groupId,
                UserId = userId,
                Amount = amount
            };

            // Assert
            Assert.Equal(groupId, createExpenseSplitDto.GroupId);
            Assert.Equal(userId, createExpenseSplitDto.UserId);
            Assert.Equal(amount, createExpenseSplitDto.Amount);
        }

        [Fact]
        public void CreateExpenseSplitDto_SetNullProperties_Success()
        {
            // Act
            var createExpenseSplitDto = new CreateExpenseSplitDto
            {
                GroupId = null,
                UserId = null,
                Amount = 0
            };

            // Assert
            Assert.Null(createExpenseSplitDto.GroupId);
            Assert.Null(createExpenseSplitDto.UserId);
            Assert.Equal(0, createExpenseSplitDto.Amount);
        }

        [Fact]
        public void CreateExpenseSplitDto_SetEmptyStringProperties_Success()
        {
            // Act
            var createExpenseSplitDto = new CreateExpenseSplitDto
            {
                GroupId = string.Empty,
                UserId = string.Empty,
                Amount = 0
            };

            // Assert
            Assert.Equal(string.Empty, createExpenseSplitDto.GroupId);
            Assert.Equal(string.Empty, createExpenseSplitDto.UserId);
            Assert.Equal(0, createExpenseSplitDto.Amount);
        }

        [Fact]
        public void CreateExpenseSplitDto_SetNegativeAmount_Success()
        {
            // Arrange
            var amount = -50.75m;

            // Act
            var createExpenseSplitDto = new CreateExpenseSplitDto
            {
                GroupId = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Amount = amount
            };

            // Assert
            Assert.Equal(amount, createExpenseSplitDto.Amount);
        }
    }
}
