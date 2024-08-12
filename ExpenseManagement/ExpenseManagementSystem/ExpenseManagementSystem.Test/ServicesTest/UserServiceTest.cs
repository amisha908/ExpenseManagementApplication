using AutoMapper;
using ExpenseManagement.BLL.Services.Implementation;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ServicesTest
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IExpenseSplitRepository> _expenseSplitRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _expenseSplitRepositoryMock = new Mock<IExpenseSplitRepository>();
            _userService = new UserService(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _expenseSplitRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_PositiveTest()
        {
            // Arrange
            var usersFromRepository = new List<ApplicationUser>
            {
        new ApplicationUser { Id = "1", Name = "User1", Email = "user1@example.com" },
        new ApplicationUser { Id = "2", Name = "User2", Email = "user2@example.com" }
            };
            _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(usersFromRepository);

            var userDtosExpected = usersFromRepository.Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email }).ToList();

            _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<ApplicationUser>>()))
                .Returns(userDtosExpected);

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Equal(userDtosExpected.Count, result.Count());
            for (int i = 0; i < userDtosExpected.Count; i++)
            {
                Assert.Equal(userDtosExpected[i].Id, result.ElementAt(i).Id);
                Assert.Equal(userDtosExpected[i].Name, result.ElementAt(i).Name);
                Assert.Equal(userDtosExpected[i].Email, result.ElementAt(i).Email);
            }
        }

        [Fact]
        public async Task GetUserTotalOwesAsync_PositiveTest()
        {
            // Arrange
            string userId = "user1";
            var expenseSplitsFromRepository = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "split1", UserId = "user1", Owe = 50, Own = 0 },
                new ExpenseSplit { Id = "split2", UserId = "user1", Owe = 30, Own = 0 }
            };
            _expenseSplitRepositoryMock.Setup(repo => repo.GetExpenseSplitsByUserIdAsync(userId)).ReturnsAsync(expenseSplitsFromRepository);

            decimal expectedTotalOwes = expenseSplitsFromRepository.Sum(es => es.Owe);

            // Act
            var result = await _userService.GetUserTotalOwesAsync(userId);

            // Assert
            Assert.Equal(expectedTotalOwes, result);
        }

        [Fact]
        public async Task GetUserTotalOwnsAsync_PositiveTest()
        {
            // Arrange
            string userId = "user1";
            var expenseSplitsFromRepository = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "split1", UserId = "user1", Owe = 0, Own = 80 },
                new ExpenseSplit { Id = "split2", UserId = "user1", Owe = 0, Own = 40 }
            };
            _expenseSplitRepositoryMock.Setup(repo => repo.GetExpenseSplitsByUserIdAsync(userId)).ReturnsAsync(expenseSplitsFromRepository);

            decimal expectedTotalOwns = expenseSplitsFromRepository.Sum(es => es.Own);

            // Act
            var result = await _userService.GetUserTotalOwnsAsync(userId);

            // Assert
            Assert.Equal(expectedTotalOwns, result);
        }

        // Negative Test Cases

        [Fact]
        public async Task GetAllUsersAsync_NoUsersFoundTest()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(new List<ApplicationUser>());

            // Act
            var result = await _userService.GetAllUsersAsync();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUserTotalOwesAsync_NoExpenseSplitsFoundTest()
        {
            // Arrange
            string userId = "user1";
            _expenseSplitRepositoryMock.Setup(repo => repo.GetExpenseSplitsByUserIdAsync(userId)).ReturnsAsync(new List<ExpenseSplit>());

            // Act
            var result = await _userService.GetUserTotalOwesAsync(userId);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task GetUserTotalOwnsAsync_NoExpenseSplitsFoundTest()
        {
            // Arrange
            string userId = "user1";
            _expenseSplitRepositoryMock.Setup(repo => repo.GetExpenseSplitsByUserIdAsync(userId)).ReturnsAsync(new List<ExpenseSplit>());

            // Act
            var result = await _userService.GetUserTotalOwnsAsync(userId);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
