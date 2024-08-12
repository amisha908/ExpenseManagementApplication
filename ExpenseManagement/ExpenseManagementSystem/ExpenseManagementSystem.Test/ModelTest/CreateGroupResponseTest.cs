using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class CreateGroupResponseTest
    {
        [Fact]
        public void Should_SetAndGet_Id()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedId = "group123";

            // Act
            createGroupResponse.Id = expectedId;
            var actualId = createGroupResponse.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void Should_SetAndGet_Name()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedName = "Test Group";

            // Act
            createGroupResponse.Name = expectedName;
            var actualName = createGroupResponse.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Should_SetAndGet_Description()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedDescription = "Test Description";

            // Act
            createGroupResponse.Description = expectedDescription;
            var actualDescription = createGroupResponse.Description;

            // Assert
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        public void Should_SetAndGet_CreatedDate()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedDate = new DateTime(2023, 7, 1);

            // Act
            createGroupResponse.CreatedDate = expectedDate;
            var actualDate = createGroupResponse.CreatedDate;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void Should_SetAndGet_Members()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedMembers = new List<MemberDto>
            {
                new MemberDto { Id = "member1" },
                new MemberDto { Id = "member2" }
            };

            // Act
            createGroupResponse.Members = expectedMembers;
            var actualMembers = createGroupResponse.Members;

            // Assert
            Assert.Equal(expectedMembers, actualMembers);
        }

        [Fact]
        public void Should_SetAndGet_Expenses()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedExpenses = new List<ExpenseDto>
            {
                new ExpenseDto { Id = "expense1" },
                new ExpenseDto { Id = "expense2" }
            };

            // Act
            createGroupResponse.Expenses = expectedExpenses;
            var actualExpenses = createGroupResponse.Expenses;

            // Assert
            Assert.Equal(expectedExpenses, actualExpenses);
        }

        [Fact]
        public void Should_SetAndGet_MemberIds()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedMemberIds = new List<string> { "member1", "member2" };

            // Act
            createGroupResponse.MemberIds = expectedMemberIds;
            var actualMemberIds = createGroupResponse.MemberIds;

            // Assert
            Assert.Equal(expectedMemberIds, actualMemberIds);
        }

        [Fact]
        public void Should_Allow_Null_Id()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();

            // Act
            createGroupResponse.Id = null;
            var actualId = createGroupResponse.Id;

            // Assert
            Assert.Null(actualId);
        }

        [Fact]
        public void Should_Allow_Null_Name()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();

            // Act
            createGroupResponse.Name = null;
            var actualName = createGroupResponse.Name;

            // Assert
            Assert.Null(actualName);
        }

        [Fact]
        public void Should_Allow_Null_Description()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();

            // Act
            createGroupResponse.Description = null;
            var actualDescription = createGroupResponse.Description;

            // Assert
            Assert.Null(actualDescription);
        }

        [Fact]
        public void Should_Allow_Invalid_CreatedDate()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();
            var expectedDate = DateTime.MinValue;

            // Act
            createGroupResponse.CreatedDate = expectedDate;
            var actualDate = createGroupResponse.CreatedDate;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }

        [Fact]
        public void Should_Allow_Null_Members()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();

            // Act
            createGroupResponse.Members = null;
            var actualMembers = createGroupResponse.Members;

            // Assert
            Assert.Null(actualMembers);
        }

        [Fact]
        public void Should_Allow_Null_Expenses()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();

            // Act
            createGroupResponse.Expenses = null;
            var actualExpenses = createGroupResponse.Expenses;

            // Assert
            Assert.Null(actualExpenses);
        }

        [Fact]
        public void Should_Allow_Null_MemberIds()
        {
            // Arrange
            var createGroupResponse = new CreateGroupResponse();

            // Act
            createGroupResponse.MemberIds = null;
            var actualMemberIds = createGroupResponse.MemberIds;

            // Assert
            Assert.Null(actualMemberIds);
        }
    }
}
