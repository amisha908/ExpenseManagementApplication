using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.RepositoryTest
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task GetAllUsersAsync_ReturnsAllUsers()
        {
            // Arrange
            var usersData = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "user1", Name = "User 1" },
                new ApplicationUser { Id = "user2", Name = "User 2" }
            };

            var options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: "GetAllUsersAsync_Db")
                .Options;

            using (var context = new ExpenseSharingContext(options))
            {
                await context.Users.AddRangeAsync(usersData);
                await context.SaveChangesAsync();
            }

            using (var context = new ExpenseSharingContext(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.GetAllUsersAsync();

                // Assert
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser()
        {
            // Arrange
            var userData = new ApplicationUser { Id = "user1", Name = "User 1" };

            var options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: "GetUserByIdAsync_Db")
                .Options;

            using (var context = new ExpenseSharingContext(options))
            {
                await context.Users.AddAsync(userData);
                await context.SaveChangesAsync();
            }

            using (var context = new ExpenseSharingContext(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.GetUserByIdAsync("user1");

                // Assert
                Assert.NotNull(result);
                Assert.Equal("User 1", result.Name);
            }
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsNullForNonExistingId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: "GetUserByIdAsync_Db")
                .Options;

            using (var context = new ExpenseSharingContext(options))
            {
                var userRepository = new UserRepository(context);

                // Act
                var result = await userRepository.GetUserByIdAsync("non-existing-id");

                // Assert
                Assert.Null(result);
            }
        }
    }
}
