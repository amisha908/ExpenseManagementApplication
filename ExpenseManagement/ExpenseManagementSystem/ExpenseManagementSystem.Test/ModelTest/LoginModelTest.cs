using ExpenseManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class LoginModelTest
    {
        [Fact]
        public void Should_SetAndGet_Email()
        {
            // Arrange
            var loginModel = new LoginModel();
            var expectedEmail = "test@example.com";

            // Act
            loginModel.Email = expectedEmail;
            var actualEmail = loginModel.Email;

            // Assert
            Assert.Equal(expectedEmail, actualEmail);
        }

        [Fact]
        public void Should_SetAndGet_Password()
        {
            // Arrange
            var loginModel = new LoginModel();
            var expectedPassword = "password123";

            // Act
            loginModel.Password = expectedPassword;
            var actualPassword = loginModel.Password;

            // Assert
            Assert.Equal(expectedPassword, actualPassword);
        }

        [Fact]
        public void Should_Allow_Null_Email()
        {
            // Arrange
            var loginModel = new LoginModel();

            // Act
            loginModel.Email = null;
            var actualEmail = loginModel.Email;

            // Assert
            Assert.Null(actualEmail);
        }

        [Fact]
        public void Should_Allow_Null_Password()
        {
            // Arrange
            var loginModel = new LoginModel();

            // Act
            loginModel.Password = null;
            var actualPassword = loginModel.Password;

            // Assert
            Assert.Null(actualPassword);
        }
    }
}
