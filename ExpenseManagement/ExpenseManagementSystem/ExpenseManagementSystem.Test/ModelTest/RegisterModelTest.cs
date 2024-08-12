using ExpenseManagementSystem.ExpenseManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class RegisterModelTest
    {
        [Fact]
        public void Should_SetAndGet_Name()
        {
            // Arrange
            var registerModel = new RegisterModel();
            var expectedName = "John Doe";

            // Act
            registerModel.Name = expectedName;
            var actualName = registerModel.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Should_SetAndGet_Email()
        {
            // Arrange
            var registerModel = new RegisterModel();
            var expectedEmail = "john.doe@example.com";

            // Act
            registerModel.Email = expectedEmail;
            var actualEmail = registerModel.Email;

            // Assert
            Assert.Equal(expectedEmail, actualEmail);
        }

        [Fact]
        public void Should_SetAndGet_Password()
        {
            // Arrange
            var registerModel = new RegisterModel();
            var expectedPassword = "securepassword123";

            // Act
            registerModel.Password = expectedPassword;
            var actualPassword = registerModel.Password;

            // Assert
            Assert.Equal(expectedPassword, actualPassword);
        }

        [Fact]
        public void Should_Allow_Null_Name()
        {
            // Arrange
            var registerModel = new RegisterModel();

            // Act
            registerModel.Name = null;
            var actualName = registerModel.Name;

            // Assert
            Assert.Null(actualName);
        }

        [Fact]
        public void Should_Allow_Null_Email()
        {
            // Arrange
            var registerModel = new RegisterModel();

            // Act
            registerModel.Email = null;
            var actualEmail = registerModel.Email;

            // Assert
            Assert.Null(actualEmail);
        }

        [Fact]
        public void Should_Allow_Null_Password()
        {
            // Arrange
            var registerModel = new RegisterModel();

            // Act
            registerModel.Password = null;
            var actualPassword = registerModel.Password;

            // Assert
            Assert.Null(actualPassword);
        }
    }
}
