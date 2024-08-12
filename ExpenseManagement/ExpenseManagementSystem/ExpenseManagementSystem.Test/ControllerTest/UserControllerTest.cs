using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ControllerTest
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _mockUserService = new Mock<IUserService>();
            _controller = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async Task GetAllUsers_Returns_OkResult()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto { Id = "1", Name = "User 1" },
                new UserDto { Id = "2", Name = "User 2" }
            };
            _mockUserService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task GetAllUsers_Returns_NotFoundResult()
        {
            // Arrange
            _mockUserService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync((IEnumerable<UserDto>)null);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetUserTotalOwes_ValidUserId_Returns_OkResult()
        {
            // Arrange
            var userId = "1";
            var totalOwes = 100.50m;
            _mockUserService.Setup(service => service.GetUserTotalOwesAsync(userId)).ReturnsAsync(totalOwes);

            // Act
            var result = await _controller.GetUserTotalOwes(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(totalOwes, model);
        }

        [Fact]
        public async Task GetUserTotalOwes_InvalidUserId_Returns_OkResult_WithZero()
        {
            // Arrange
            var userId = "invalid_id";
            _mockUserService.Setup(service => service.GetUserTotalOwesAsync(userId)).ReturnsAsync(0m);

            // Act
            var result = await _controller.GetUserTotalOwes(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(0m, model);
        }

        [Fact]
        public async Task GetUserTotalOwns_ValidUserId_Returns_OkResult()
        {
            // Arrange
            var userId = "1";
            var totalOwns = 150.75m;
            _mockUserService.Setup(service => service.GetUserTotalOwnsAsync(userId)).ReturnsAsync(totalOwns);

            // Act
            var result = await _controller.GetUserTotalOwns(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(totalOwns, model);
        }

        [Fact]
        public async Task GetUserTotalOwns_InvalidUserId_Returns_OkResult_WithZero()
        {
            // Arrange
            var userId = "invalid_id";
            _mockUserService.Setup(service => service.GetUserTotalOwnsAsync(userId)).ReturnsAsync(0m);

            // Act
            var result = await _controller.GetUserTotalOwns(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(0m, model);
        }
    }
}
