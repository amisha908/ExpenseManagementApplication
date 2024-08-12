using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.RepositoryTest
{
    public class ExpenseSplitRepositoryTests
    {
        private readonly DbContextOptions<ExpenseSharingContext> _options;

        public ExpenseSplitRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: "ExpenseSharingTest")
                .Options;
        }

        [Fact]
        public async Task AddExpenseSplitAsync_ShouldAddExpenseSplit()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = "user1", Owe = 50.0m };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var repository = new ExpenseSplitRepository(context);

                // Act
                await repository.AddExpenseSplitAsync(expenseSplit);

                // Assert
                var addedSplit = await context.ExpenseSplits.FindAsync("1");
                Assert.NotNull(addedSplit);
                Assert.Equal(expenseSplit.ExpenseId, addedSplit.ExpenseId);
                Assert.Equal(expenseSplit.UserId, addedSplit.UserId);
                Assert.Equal(expenseSplit.Owe, addedSplit.Owe);
            }
        }

        [Fact]
        public async Task GetSplitByExpenseIdAndUserIdAsync_ShouldReturnExpenseSplit()
        {
            // Arrange
            var expenseSplit1 = new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = "user1", Owe = 50.0m };
            var expenseSplit2 = new ExpenseSplit { Id = "2", ExpenseId = "expense1", UserId = "user2", Owe = 30.0m };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.ExpenseSplits.AddRange(new[] { expenseSplit1, expenseSplit2 });
                context.SaveChanges();

                var repository = new ExpenseSplitRepository(context);

                // Act
                var result1 = await repository.GetSplitByExpenseIdAndUserIdAsync("expense1", "user1");
                var result2 = await repository.GetSplitByExpenseIdAndUserIdAsync("expense1", "user2");

                // Assert
                Assert.NotNull(result1);
                Assert.NotNull(result2);
                Assert.Equal(expenseSplit1.ExpenseId, result1.ExpenseId);
                Assert.Equal(expenseSplit1.UserId, result1.UserId);
                Assert.Equal(expenseSplit1.Owe, result1.Owe);
                Assert.Equal(expenseSplit2.ExpenseId, result2.ExpenseId);
                Assert.Equal(expenseSplit2.UserId, result2.UserId);
                Assert.Equal(expenseSplit2.Owe, result2.Owe);
            }
        }

        [Fact]
        public async Task GetAllExpenseSplitsAsync_ShouldReturnAllExpenseSplits()
        {
            // Arrange
            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = "user1", Owe = 50.0m },
                new ExpenseSplit { Id = "2", ExpenseId = "expense2", UserId = "user2", Owe = 30.0m }
            };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.ExpenseSplits.AddRange(expenseSplits);
                context.SaveChanges();

                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetAllExpenseSplitsAsync();

                // Assert
                Assert.Equal(2, result.Count);
                Assert.Contains(result, es => es.ExpenseId == "expense1");
                Assert.Contains(result, es => es.ExpenseId == "expense2");
            }
        }

        [Fact]
        public async Task GetSplitsByExpenseIdAsync_ShouldReturnSplitsForExpenseId()
        {
            // Arrange
            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = "user1", Owe = 50.0m },
                new ExpenseSplit { Id = "2", ExpenseId = "expense1", UserId = "user2", Owe = 30.0m }
            };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.ExpenseSplits.AddRange(expenseSplits);
                context.SaveChanges();

                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetSplitsByExpenseIdAsync("expense1");

                // Assert
                Assert.Equal(2, result.Count);
                Assert.Contains(result, es => es.UserId == "user1");
                Assert.Contains(result, es => es.UserId == "user2");
            }
        }

        [Fact]
        public async Task GetTotalAmountOwedByUserAsync_ShouldReturnTotalAmountOwed()
        {
            // Arrange
            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = "user1", Owe = 50.0m },
                new ExpenseSplit { Id = "2", ExpenseId = "expense2", UserId = "user1", Owe = 30.0m }
            };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.ExpenseSplits.AddRange(expenseSplits);
                context.SaveChanges();

                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetTotalAmountOwedByUserAsync("user1");

                // Assert
                Assert.Equal(80.0m, result);
            }
        }

        [Fact]
        public async Task GetSplitByExpenseIdAndUserIdAsync_ReturnsNullWhenNoMatch()
        {
            // Arrange
            using (var context = new ExpenseSharingContext(_options))
            {
                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetSplitByExpenseIdAndUserIdAsync("nonexistent", "user1");

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetSplitsByExpenseIdAsync_ReturnsEmptyListWhenNoSplits()
        {
            // Arrange
            using (var context = new ExpenseSharingContext(_options))
            {
                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetSplitsByExpenseIdAsync("nonexistent");

                // Assert
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task GetTotalAmountOwedByUserAsync_ReturnsZeroWhenNoSplits()
        {
            // Arrange
            using (var context = new ExpenseSharingContext(_options))
            {
                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetTotalAmountOwedByUserAsync("nonexistent");

                // Assert
                Assert.Equal(0.0m, result);
            }

        }
        [Fact]
        public async Task GetExpenseSplitsByUserIdAsync_ShouldReturnExpenseSplitsForUserId()
        {
            // Arrange
            var userId = "user1";
            var expenseSplits = new List<ExpenseSplit>
    {
        new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = userId, Owe = 50.0m },
        new ExpenseSplit { Id = "2", ExpenseId = "expense2", UserId = userId, Owe = 30.0m }
    };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.ExpenseSplits.AddRange(expenseSplits);
                context.SaveChanges();

                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetExpenseSplitsByUserIdAsync(userId);

                // Assert
                Assert.Equal(2, result.Count());
                Assert.Contains(result, es => es.ExpenseId == "expense1");
                Assert.Contains(result, es => es.ExpenseId == "expense2");
            }
        }
        [Fact]
        public async Task GetExpenseSplitsByUserIdAsync_ShouldReturnEmptyListForNonExistingUserId()
        {
            // Arrange
            var userId = "nonexistent";

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var repository = new ExpenseSplitRepository(context);

                // Act
                var result = await repository.GetExpenseSplitsByUserIdAsync(userId);

                // Assert
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task UpdateExpenseSplitAsync_ShouldUpdateExpenseSplit()
        {
            // Arrange
            var expenseSplit = new ExpenseSplit { Id = "1", ExpenseId = "expense1", UserId = "user1", Owe = 50.0m };

            using (var context = new ExpenseSharingContext(_options))
            {
                // Ensure database is clean before adding entities
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.ExpenseSplits.Add(expenseSplit);
                context.SaveChanges();

                var repository = new ExpenseSplitRepository(context);

                // Modify the split
                expenseSplit.Owe = 25.0m;

                // Act
                await repository.UpdateExpenseSplitAsync(expenseSplit);

                // Assert
                var updatedSplit = await context.ExpenseSplits.FindAsync("1");
                Assert.NotNull(updatedSplit);
                Assert.Equal(25.0m, updatedSplit.Owe);
            }
        }

       



    }
}
