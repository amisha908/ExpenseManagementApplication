using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ServicesTest
{
    public class ExpenseSplitServiceTests
    {
        private readonly Mock<IExpenseSplitRepository> _mockExpenseSplitRepository;
        private readonly ExpenseSplitService _expenseSplitService;

        public ExpenseSplitServiceTests()
        {
            _mockExpenseSplitRepository = new Mock<IExpenseSplitRepository>();
            _expenseSplitService = new ExpenseSplitService(_mockExpenseSplitRepository.Object);
        }

        [Fact]
        public async Task GetAllExpenseSplitsAsync_ReturnsExpenseSplits_WhenRepositoryHasData()
        {
            // Arrange
            var expenseSplits = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "1", Amount = 100 },
                new ExpenseSplit { Id = "2", Amount = 200 }
            };
            _mockExpenseSplitRepository.Setup(repo => repo.GetAllExpenseSplitsAsync())
                                       .ReturnsAsync(expenseSplits);

            // Act
            var result = await _expenseSplitService.GetAllExpenseSplitsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(100, result[0].Amount);
            Assert.Equal(200, result[1].Amount);
        }

        [Fact]
        public async Task GetAllExpenseSplitsAsync_ReturnsEmpty_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            _mockExpenseSplitRepository.Setup(repo => repo.GetAllExpenseSplitsAsync())
                                       .ReturnsAsync(new List<ExpenseSplit>());

            // Act
            var result = await _expenseSplitService.GetAllExpenseSplitsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllExpenseSplitsAsync_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            _mockExpenseSplitRepository.Setup(repo => repo.GetAllExpenseSplitsAsync())
                                       .ThrowsAsync(new System.Exception("Repository failure"));

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(async () =>
            {
                await _expenseSplitService.GetAllExpenseSplitsAsync();
            });
        }
    }
}
