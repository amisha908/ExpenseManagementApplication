using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class UserBalanceTests
    {
        [Fact]
        public void UserBalance_PositiveCase()
        {
            // Arrange
            var userBalance = new UserBalance
            {
                UserId = "user123",
                Owns = 100.0m,
                Owes = 50.0m
            };

            // Act

            // Assert
            Assert.Equal("user123", userBalance.UserId);
            Assert.Equal(100.0m, userBalance.Owns);
            Assert.Equal(50.0m, userBalance.Owes);
        }

        [Fact]
        public void UserBalance_NegativeCase()
        {
            // Arrange
            var userBalance = new UserBalance
            {
                UserId = "user456",
                Owns = 75.0m,
                Owes = 75.0m
            };

            // Act

            // Assert
            Assert.NotEqual(80.0m, userBalance.Owns); // Ensure Owns is not equal to a different value
            Assert.NotEqual(80.0m, userBalance.Owes); // Ensure Owes is not equal to a different value
        }

        [Fact]
        public void UserBalance_DefaultValues()
        {
            // Arrange
            var userBalance = new UserBalance();

            // Act

            // Assert
            Assert.Null(userBalance.UserId);
            Assert.Equal(0.0m, userBalance.Owns);
            Assert.Equal(0.0m, userBalance.Owes);
        }
    }
}
