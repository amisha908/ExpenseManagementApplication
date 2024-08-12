using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagementSystem.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ControllerTest
{
    public class ExpenseControllerTests
    {
        private readonly Mock<IExpenseService> _mockExpenseService;
        private readonly ExpenseController _controller;

        public ExpenseControllerTests()
        {
            _mockExpenseService = new Mock<IExpenseService>();
            _controller = new ExpenseController(_mockExpenseService.Object);
        }

        // Test for AddExpense method
        [Fact]
        public async Task AddExpense_ReturnsOkResult_WithCreatedExpense()
        {
            // Arrange
            var createExpenseDto = new CreateExpenseDto { Amount = 100, Description = "Test" };
            var expenseDto = new ExpenseDto { Id = "1", Amount = 100, Description = "Test" };
            _mockExpenseService.Setup(s => s.AddExpenseAsync(createExpenseDto)).ReturnsAsync(expenseDto);

            // Act
            var result = await _controller.AddExpense(createExpenseDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ExpenseDto>(okResult.Value);
            Assert.Equal(expenseDto, returnValue);
        }

        // Test for AddExpenseSplits method
        [Fact]
        public async Task AddExpenseSplits_ReturnsOkResult_WhenSplitsAddedSuccessfully()
        {
            // Arrange
            var request = new AddExpenseSplitsRequest { GroupId = "test-group-id" };
            _mockExpenseService.Setup(s => s.AddExpenseSplitsAsync(request.GroupId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddExpenseSplits(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task AddExpenseSplits_ReturnsBadRequest_WhenGroupIdIsNull()
        {
            // Arrange
            var request = new AddExpenseSplitsRequest { GroupId = null };

            // Act
            var result = await _controller.AddExpenseSplits(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("GroupId is required", badRequestResult.Value);
        }

        [Fact]
        public async Task AddExpenseSplits_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var request = new AddExpenseSplitsRequest { GroupId = "test-group-id" };
            _mockExpenseService.Setup(s => s.AddExpenseSplitsAsync(request.GroupId)).ThrowsAsync(new Exception("Test exception"));

            // Act
            var result = await _controller.AddExpenseSplits(request);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Internal server error: Test exception", objectResult.Value);
        }

        // Test for GetExpensesByGroupId method
        [Fact]
        public async Task GetExpensesByGroupId_ReturnsOkResult_WithListOfExpenses()
        {
            // Arrange
            var groupId = "test-group-id";
            var expenses = new List<ExpenseDto>
        {
            new ExpenseDto { Id = "1", Amount = 100, Description = "Test1" },
            new ExpenseDto { Id = "2", Amount = 200, Description = "Test2" }
        };
            _mockExpenseService.Setup(s => s.GetExpensesByGroupIdAsync(groupId)).ReturnsAsync(expenses);

            // Act
            var result = await _controller.GetExpensesByGroupId(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ExpenseDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetExpensesByGroupId_ReturnsNotFound_WhenGroupDoesNotExist()
        {
            // Arrange
            var groupId = "non-existing-group-id";
            _mockExpenseService.Setup(s => s.GetExpensesByGroupIdAsync(groupId)).ReturnsAsync((List<ExpenseDto>)null);

            // Act
            var result = await _controller.GetExpensesByGroupId(groupId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        // Test for SettleExpenses method
        [Fact]
        public async Task SettleExpenses_ReturnsOkResult_WhenExpensesSettledSuccessfully()
        {
            // Arrange
            var expenseIds = new List<string> { "1", "2" };
            _mockExpenseService.Setup(s => s.SettleExpensesAsync(expenseIds)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.SettleExpenses(expenseIds);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}