using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class UpdateGroupRequestTest
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void UpdateGroupRequest_ValidModel_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Valid Group Name",
                Description = "This is a valid description.",
                CreatedDate = DateTime.Now,
                MemberIds = new List<string> { Guid.NewGuid().ToString() }
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Empty(validationResults);
        }


        [Fact]
        public void UpdateGroupRequest_MissingId_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Name = "Valid Group Name",
                Description = "This is a valid description.",
                CreatedDate = DateTime.Now,
                MemberIds = new List<string> { Guid.NewGuid().ToString() }
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(UpdateGroupRequest.Id)));
        }

        [Fact]
        public void UpdateGroupRequest_NameTooShort_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "A",
                Description = "This is a valid description.",
                CreatedDate = DateTime.Now,
                MemberIds = new List<string> { Guid.NewGuid().ToString() }
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(UpdateGroupRequest.Name)));
        }

        [Fact]
        public void UpdateGroupRequest_NameTooLong_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = new string('A', 101),
                Description = "This is a valid description.",
                CreatedDate = DateTime.Now,
                MemberIds = new List<string> { Guid.NewGuid().ToString() }
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(UpdateGroupRequest.Name)));
        }

        [Fact]
        public void UpdateGroupRequest_DescriptionTooLong_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Valid Group Name",
                Description = new string('A', 501),
                CreatedDate = DateTime.Now,
                MemberIds = new List<string> { Guid.NewGuid().ToString() }
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(UpdateGroupRequest.Description)));
        }

      

        [Fact]
        public void UpdateGroupRequest_MemberIdsTooFew_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Valid Group Name",
                Description = "This is a valid description.",
                CreatedDate = DateTime.Now,
                MemberIds = new List<string>() // Empty list
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(UpdateGroupRequest.MemberIds)));
        }

        [Fact]
        public void UpdateGroupRequest_MemberIdsTooMany_ShouldHaveValidationErrors()
        {
            // Arrange
            var model = new UpdateGroupRequest
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Valid Group Name",
                Description = "This is a valid description.",
                CreatedDate = DateTime.Now,
                MemberIds = Enumerable.Range(1, 11).Select(i => Guid.NewGuid().ToString()).ToList()
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains(nameof(UpdateGroupRequest.MemberIds)));
        }

    }
}
