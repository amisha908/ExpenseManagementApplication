using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class CreateGroupRequestTest
    {
        private void ValidateModel(object model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model);
            results = new List<ValidationResult>();
            Validator.TryValidateObject(model, context, results, true);
        }

        [Fact]
        public void Should_SetAndValidate_ValidName()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Should_SetAndValidate_ValidDescription()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", Description = "Test Description", CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Should_SetAndValidate_ValidCreatedDate()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Should_SetAndValidate_ValidMemberIds()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1", "member2" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void Should_Invalidate_ShortName()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Te", CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(CreateGroupRequest.Name)) && r.ErrorMessage.Contains("minimum length"));
        }

        [Fact]
        public void Should_Invalidate_LongName()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = new string('A', 101), CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(CreateGroupRequest.Name)) && r.ErrorMessage.Contains("maximum length"));
        }

        [Fact]
        public void Should_Invalidate_LongDescription()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", Description = new string('A', 501), CreatedDate = DateTime.Now, MemberIds = new List<string> { "member1" } };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(CreateGroupRequest.Description)) && r.ErrorMessage.Contains("maximum length"));
        }

       // [Fact]
        //public void Should_Invalidate_NullCreatedDate()
        //{
        //    // Arrange
        //    var model = new CreateGroupRequest { Name = "Test Group", MemberIds = new List<string> { "member1" } };

        //    // Act
        //    ValidateModel(model, out var results);

        //    // Assert
        //    Assert.NotEmpty(results);
        //    Assert.Contains(results, r => r.MemberNames.Contains(nameof(CreateGroupRequest.CreatedDate)) && r.ErrorMessage.Contains("required"));
        //}

        [Fact]
        public void Should_Invalidate_EmptyMemberIds()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", CreatedDate = DateTime.Now, MemberIds = new List<string>() };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(CreateGroupRequest.MemberIds)) && r.ErrorMessage.Contains("minimum length"));
        }

        [Fact]
        public void Should_Invalidate_TooManyMemberIds()
        {
            // Arrange
            var model = new CreateGroupRequest { Name = "Test Group", CreatedDate = DateTime.Now, MemberIds = new List<string>(new string[11]) };

            // Act
            ValidateModel(model, out var results);

            // Assert
            Assert.NotEmpty(results);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(CreateGroupRequest.MemberIds)) && r.ErrorMessage.Contains("maximum length"));
        }
    }
}
