using System;
using System.Net;
using System.Threading.Tasks;
using ExpenseManagementSystem.Controllers;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using Xunit;

namespace ExpenseManagementSystem.Test.ControllerTest
{
    public class AuthControllerTest
    {
        private readonly AuthController _controller;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;
        private readonly Mock<IConfiguration> _mockConfiguration;

        //public AuthControllerTest()
        //{
        //    // Setup mock objects
        //    var userStore = new Mock<IUserStore<ApplicationUser>>();
        //    _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
        //    _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(_mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
        //    _mockRoleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);
        //    _mockConfiguration = new Mock<IConfiguration>();

        //    // Setup controller with mock dependencies
        //    _controller = new AuthController(
        //        _mockUserManager.Object,
        //        _mockSignInManager.Object,
        //        _mockRoleManager.Object,
        //        _mockConfiguration.Object
        //    );
        //}
        public AuthControllerTest()
        {
            var userStore = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(_mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            // Mock IConfiguration with a valid JWT key
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.SetupGet(x => x["Jwt:Key"]).Returns("your_secret_key_here");

            _controller = new AuthController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockRoleManager.Object,
                _mockConfiguration.Object
            );
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsOk()
        {
            // Arrange
            var user = new RegisterModel { Email = "test@example.com", Password = "Pass@w0rd123", Name = "Test User" };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(user);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal(200, okResult.StatusCode);
            Assert.Contains("User registered successfully", okResult.Value.ToString());
        }
        [Fact]
        public async Task Login_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "test@test.com",
                Password = "Test@123"
            };

            _mockSignInManager.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "test@test.com",
                    UserName = "test@test.com"
                });

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_InvalidData_BadRequest()
        {
            // Arrange
            var registerModel = new RegisterModel(); // Invalid model with missing required fields

            _controller.ModelState.AddModelError("Name", "The Name field is required."); // Simulating ModelState error

            // Act
            var result = await _controller.Register(registerModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task Register_UserCreationFails_ReturnsBadRequest()
        {
            // Arrange
            var user = new RegisterModel { Email = "test@example.com", Password = "Pass@w0rd123", Name = "Test User" };
            var identityErrors = new List<IdentityError>
            {
                new IdentityError { Code = "DuplicateUserName", Description = "Username already exists" }
            };
            var identityResult = IdentityResult.Failed(identityErrors.ToArray());

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.Register(user);

            // Assert
            Assert.NotNull(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsAssignableFrom<IEnumerable<IdentityError>>(badRequestResult.Value);
            Assert.Collection(errors, error =>
            {
                Assert.Equal("DuplicateUserName", error.Code);
                Assert.Equal("Username already exists", error.Description);
            });
        }

        [Fact]
        public async Task Login_InvalidCredentials_Unauthorized()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "test@example.com",
                Password = "InvalidPassword"
            };

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed); // Ensure to return SignInResult.Failed

            // Act
            var result = await _controller.Login(loginModel) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.Unauthorized, result.StatusCode);
        }
        [Fact]
        public async Task Login_UserNotFound_ReturnsUnauthorized()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                Email = "nonexistent@test.com",
                Password = "Test@123"
            };

            _mockSignInManager.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _mockUserManager.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null); // Simulate user not found

            // Act
            var result = await _controller.Login(loginModel);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
            var unauthorizedResult = result as UnauthorizedResult;
            Assert.Equal((int)HttpStatusCode.Unauthorized, unauthorizedResult.StatusCode);
        }
        [Fact]
        public async Task Login_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var loginModel = new LoginModel(); // Invalid model with missing required fields

            _controller.ModelState.AddModelError("Email", "The Email field is required."); // Simulating ModelState error

            // Act
            var result = await _controller.Login(loginModel) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }


    }
}

