using AutoMapper;
using ExpenseManagement.BLL.Services.Implementation;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ServicesTest
{
    public class ExpenseServiceTest
    {
        private readonly Mock<IExpenseRepository> _expenseRepositoryMock;
        private readonly Mock<IExpenseSplitRepository> _expenseSplitRepositoryMock;
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ExpenseService _expenseService;

        public ExpenseServiceTest()
        {
            _expenseRepositoryMock = new Mock<IExpenseRepository>();
            _expenseSplitRepositoryMock = new Mock<IExpenseSplitRepository>();
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _mapperMock = new Mock<IMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _expenseService = new ExpenseService(
                _expenseRepositoryMock.Object,
                _expenseSplitRepositoryMock.Object,
                _groupRepositoryMock.Object,
                _mapperMock.Object,
                _userRepositoryMock.Object);
        }

        // Positive Test Cases
        [Fact]
        public async Task AddExpenseAsync_PositiveTest()
        {
            // Arrange
            var createExpenseDto = new CreateExpenseDto { GroupId = "group1", Amount = 100, Description = "Test Expense", PaidById = "user1" };
            var expense = new Expense { Id = "1", GroupId = "group1", Amount = 100, Description = "Test Expense", PaidById = "user1" };
            var expenseDto = new ExpenseDto { Id = "1", GroupId = "group1", Amount = 100, Description = "Test Expense", PaidById = "user1" };

            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync("group1")).ReturnsAsync(new Group { Id = "group1" });
            _mapperMock.Setup(m => m.Map<Expense>(createExpenseDto)).Returns(expense);
            _mapperMock.Setup(m => m.Map<ExpenseDto>(expense)).Returns(expenseDto);

            // Act
            var result = await _expenseService.AddExpenseAsync(createExpenseDto);

            // Assert
            Assert.Equal(expenseDto.Id, result.Id);
            _expenseRepositoryMock.Verify(repo => repo.AddAsync(expense), Times.Once);
        }

        [Fact]
        public async Task AddExpenseSplitsAsync_PositiveTest()
        {
            // Arrange
            var group = new Group
            {
                Id = "group1",
                Members = new List<Member>
                {
                    new Member { UserId = "user1" },
                    new Member { UserId = "user2" }
                }
            };
            var expense = new Expense { Id = "expense1", GroupId = "group1", Amount = 100, PaidById = "user1" };
            var expenses = new List<Expense> { expense };

            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync("group1")).ReturnsAsync(group);
            _expenseRepositoryMock.Setup(repo => repo.GetByGroupIdAsync("group1")).ReturnsAsync(expenses);

            // Act
            await _expenseService.AddExpenseSplitsAsync("group1");

            // Assert
            _expenseSplitRepositoryMock.Verify(repo => repo.AddExpenseSplitAsync(It.IsAny<ExpenseSplit>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GetExpensesByGroupIdAsync_PositiveTest()
        {
            // Arrange
            var groupId = "group1";
            var expense = new Expense { Id = "expense1", GroupId = groupId, Amount = 100, PaidById = "user1" };
            var expenses = new List<Expense> { expense };

            _expenseRepositoryMock.Setup(repo => repo.GetByGroupIdAsync(groupId)).ReturnsAsync(expenses);
            Console.WriteLine($"Mock setup: Expense repository should return {expenses.Count} expense(s)");

            var expenseSplit = new ExpenseSplit { UserId = "user2", ExpenseId = "expense1", Amount = 50 };
            var expenseSplits = new List<ExpenseSplit> { expenseSplit };

            _expenseSplitRepositoryMock.Setup(repo => repo.GetSplitsByExpenseIdAsync("expense1")).ReturnsAsync(expenseSplits);
            Console.WriteLine($"Mock setup: Expense split repository should return {expenseSplits.Count} split(s)");

            var user = new ApplicationUser { Id = "user2", Name = "User 2" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync("user2")).ReturnsAsync(user);
            Console.WriteLine("Mock setup: User repository should return a user");

            _mapperMock.Setup(m => m.Map<IEnumerable<ExpenseDto>>(It.IsAny<List<Expense>>()))
                .Returns((List<Expense> e) =>
                {
                    var mapped = e.Select(ex => new ExpenseDto { Id = ex.Id, GroupId = ex.GroupId, Amount = ex.Amount, PaidById = ex.PaidById }).ToList();
                    Console.WriteLine($"Mapper: Mapped {mapped.Count} expense(s)");
                    return mapped;
                });

            _mapperMock.Setup(m => m.Map<List<ExpenseSplitDto>>(It.IsAny<List<ExpenseSplit>>()))
                .Returns((List<ExpenseSplit> s) =>
                {
                    var mapped = s.Select(split => new ExpenseSplitDto { UserId = split.UserId, ExpenseId = split.ExpenseId, Amount = split.Amount }).ToList();
                    Console.WriteLine($"Mapper: Mapped {mapped.Count} expense split(s)");
                    return mapped;
                });

            // Act
            Console.WriteLine("Calling GetExpensesByGroupIdAsync");
            var result = await _expenseService.GetExpensesByGroupIdAsync(groupId);

            // Debug
            Console.WriteLine($"Result count: {result?.Count() ?? 0}");
            if (result != null && result.Any())
            {
                var firstExpense = result.First();
                Console.WriteLine($"First expense ID: {firstExpense.Id}");
                Console.WriteLine($"Splits count: {firstExpense.ExpenseSplits?.Count ?? 0}");
            }
            else
            {
                Console.WriteLine("Result is null or empty");
            }

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultExpense = result.First();
            Assert.Equal("expense1", resultExpense.Id);
            Assert.Equal(groupId, resultExpense.GroupId);
            Assert.Equal(100, resultExpense.Amount);
            Assert.Equal("user1", resultExpense.PaidById);

            Assert.NotNull(resultExpense.ExpenseSplits);
            Assert.NotEmpty(resultExpense.ExpenseSplits);
            var resultSplit = resultExpense.ExpenseSplits.First();
            Assert.Equal("user2", resultSplit.UserId);
            Assert.Equal("expense1", resultSplit.ExpenseId);
            Assert.Equal(50, resultSplit.Amount);
            Assert.Equal("User 2", resultSplit.UserName);
            Assert.Equal(50, resultSplit.Owe);
            Assert.Equal(0, resultSplit.Own);

            _expenseRepositoryMock.Verify(repo => repo.GetByGroupIdAsync(groupId), Times.Once);
            _expenseSplitRepositoryMock.Verify(repo => repo.GetSplitsByExpenseIdAsync("expense1"), Times.Once);
            _userRepositoryMock.Verify(repo => repo.GetUserByIdAsync("user2"), Times.Once);
        }
        [Fact]
        public async Task SettleExpensesAsync_PositiveTest()
        {
            // Arrange
            var expense = new Expense { Id = "expense1", GroupId = "group1", Amount = 100, PaidById = "user1", IsSettled = false };
            var splits = new List<ExpenseSplit>
            {
                new ExpenseSplit { Id = "split1", ExpenseId = "expense1", UserId = "user1", Amount = 50, Owe = 0, Own = 50 }
            };

            _expenseRepositoryMock.Setup(repo => repo.GetByIdAsync("expense1")).ReturnsAsync(expense);
            _expenseSplitRepositoryMock.Setup(repo => repo.GetSplitsByExpenseIdAsync("expense1")).ReturnsAsync(splits);

            // Act
            await _expenseService.SettleExpensesAsync(new List<string> { "expense1" });

            // Assert
            Assert.True(expense.IsSettled);
            _expenseRepositoryMock.Verify(repo => repo.UpdateAsync(expense), Times.Once);
            _expenseSplitRepositoryMock.Verify(repo => repo.UpdateExpenseSplitAsync(It.IsAny<ExpenseSplit>()), Times.Exactly(1));
        }

        // Negative Test Cases
        [Fact]
        public async Task AddExpenseAsync_GroupNotFoundTest()
        {
            // Arrange
            var createExpenseDto = new CreateExpenseDto { GroupId = "group1", Amount = 100, Description = "Test Expense", PaidById = "user1" };

            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync("group1")).ReturnsAsync((Group)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _expenseService.AddExpenseAsync(createExpenseDto));
        }

        [Fact]
        public async Task AddExpenseSplitsAsync_GroupNotFoundTest()
        {
            // Arrange
            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync("group1")).ReturnsAsync((Group)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await _expenseService.AddExpenseSplitsAsync("group1"));
        }

        [Fact]
        public async Task AddExpenseSplitsAsync_NoMembersFoundTest()
        {
            // Arrange
            var group = new Group { Id = "group1", Members = new List<Member>() };

            _groupRepositoryMock.Setup(repo => repo.GetGroupByIdAsync("group1")).ReturnsAsync(group);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _expenseService.AddExpenseSplitsAsync("group1"));
        }

        [Fact]
        public async Task GetExpensesByGroupIdAsync_NoExpensesFoundTest()
        {
            // Arrange
            var expenses = new List<Expense>();

            _expenseRepositoryMock.Setup(repo => repo.GetByGroupIdAsync("group1")).ReturnsAsync(expenses);

            // Act
            var result = await _expenseService.GetExpensesByGroupIdAsync("group1");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task SettleExpensesAsync_ExpenseNotFoundTest()
        {
            // Arrange
            _expenseRepositoryMock.Setup(repo => repo.GetByIdAsync("expense1")).ReturnsAsync((Expense)null);

            // Act
            await _expenseService.SettleExpensesAsync(new List<string> { "expense1" });

            // Assert
            _expenseRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Expense>()), Times.Never);
            _expenseSplitRepositoryMock.Verify(repo => repo.UpdateExpenseSplitAsync(It.IsAny<ExpenseSplit>()), Times.Never);
        }
        [Fact]
        public async Task CalculateOweAmount_UserHasSplit_ShouldReturnOweAmount()
        {
            // Arrange
            var userId = "user1";
            var expenseId = "expense1";
            var split = new ExpenseSplit { UserId = userId, ExpenseId = expenseId, Owe = 50 }; // Example split with Owe amount

            _expenseSplitRepositoryMock.Setup(repo => repo.GetSplitByExpenseIdAndUserIdAsync(expenseId, userId)).ReturnsAsync(split);

            // Act
            var result = await _expenseService.CalculateOweAmount(userId, expenseId);

            // Assert
            Assert.Equal(split.Owe, result);
            _expenseSplitRepositoryMock.Verify(repo => repo.GetSplitByExpenseIdAndUserIdAsync(expenseId, userId), Times.Once);
        }
        [Fact]
        public async Task CalculateOweAmount_UserDoesNotHaveSplit_ShouldReturnZero()
        {
            // Arrange
            var userId = "user1";
            var expenseId = "expense1";

            _expenseSplitRepositoryMock.Setup(repo => repo.GetSplitByExpenseIdAndUserIdAsync(expenseId, userId)).ReturnsAsync((ExpenseSplit)null); // No split found

            // Act
            var result = await _expenseService.CalculateOweAmount(userId, expenseId);

            // Assert
            Assert.Equal(0, result);
            _expenseSplitRepositoryMock.Verify(repo => repo.GetSplitByExpenseIdAndUserIdAsync(expenseId, userId), Times.Once);
        }
        [Fact]
        public void CalculateSplitOwnership_UserPaidExpense_ShouldSetOwnAmount()
        {
            // Arrange
            var expenseDto = new ExpenseDto
            {
                Id = "expense1",
                Amount = 100,
                PaidById = "user1"
            };

            var split = new ExpenseSplitDto
            {
                UserId = "user1",
                Amount = 50
            };

            // Act
            _expenseService.CalculateSplitOwnership(expenseDto, split);

            // Assert
            Assert.Equal(50, split.Own);
            Assert.Equal(0, split.Owe);
        }
        [Fact]
        public void CalculateSplitOwnership_UserDidNotPayExpense_ShouldSetOweAmount()
        {
            // Arrange
            var expenseDto = new ExpenseDto
            {
                Id = "expense1",
                Amount = 100,
                PaidById = "user1"
            };

            var split = new ExpenseSplitDto
            {
                UserId = "user2",
                Amount = 50
            };

            // Act
            _expenseService.CalculateSplitOwnership(expenseDto, split);

            // Assert
            Assert.Equal(0, split.Own);
            Assert.Equal(50, split.Owe);
        }


    }
}
