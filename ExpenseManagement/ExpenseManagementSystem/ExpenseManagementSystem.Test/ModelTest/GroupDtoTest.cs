using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class GroupDtoTest
    {
        [Fact]
        public void Should_SetAndGet_Id()
        {
            // Arrange
            var groupDto = new GroupDto();
            var expectedId = "grp123";

            // Act
            groupDto.Id = expectedId;
            var actualId = groupDto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void Should_SetAndGet_Name()
        {
            // Arrange
            var groupDto = new GroupDto();
            var expectedName = "Friends Group";

            // Act
            groupDto.Name = expectedName;
            var actualName = groupDto.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Should_SetAndGet_Description()
        {
            // Arrange
            var groupDto = new GroupDto();
            var expectedDescription = "Group for friends to share expenses";

            // Act
            groupDto.Description = expectedDescription;
            var actualDescription = groupDto.Description;

            // Assert
            Assert.Equal(expectedDescription, actualDescription);
        }

        [Fact]
        public void Should_SetAndGet_CreatedDate()
        {
            // Arrange
            var groupDto = new GroupDto();
            var expectedCreatedDate = new DateTime(2024, 7, 8);

            // Act
            groupDto.CreatedDate = expectedCreatedDate;
            var actualCreatedDate = groupDto.CreatedDate;

            // Assert
            Assert.Equal(expectedCreatedDate, actualCreatedDate);
        }

        [Fact]
        public void Should_SetAndGet_Members()
        {
            // Arrange
            var groupDto = new GroupDto();
            var expectedMembers = new List<MemberDto>
            {
                new MemberDto { Id = "mem1", UserId = "user1", GroupId = "grp123", Name = "John" },
                new MemberDto { Id = "mem2", UserId = "user2", GroupId = "grp123", Name = "Jane" }
            };

            // Act
            groupDto.Members = expectedMembers;
            var actualMembers = groupDto.Members;

            // Assert
            Assert.Equal(expectedMembers, actualMembers);
        }

        [Fact]
        public void Should_SetAndGet_Expenses()
        {
            // Arrange
            var groupDto = new GroupDto();
            var expectedExpenses = new List<ExpenseDto>
            {
                new ExpenseDto { Id = "exp1", Amount = 100m, Date = new DateTime(2024, 7, 1), Description = "Dinner", GroupId = "grp123", PaidById = "user1" },
                new ExpenseDto { Id = "exp2", Amount = 200m, Date = new DateTime(2024, 7, 2), Description = "Lunch", GroupId = "grp123", PaidById = "user2" }
            };

            // Act
            groupDto.Expenses = expectedExpenses;
            var actualExpenses = groupDto.Expenses;

            // Assert
            Assert.Equal(expectedExpenses, actualExpenses);
        }

        [Fact]
        public void Should_Allow_Null_Id()
        {
            // Arrange
            var groupDto = new GroupDto();

            // Act
            groupDto.Id = null;
            var actualId = groupDto.Id;

            // Assert
            Assert.Null(actualId);
        }

        [Fact]
        public void Should_Allow_Null_Name()
        {
            // Arrange
            var groupDto = new GroupDto();

            // Act
            groupDto.Name = null;
            var actualName = groupDto.Name;

            // Assert
            Assert.Null(actualName);
        }

        [Fact]
        public void Should_Allow_Null_Description()
        {
            // Arrange
            var groupDto = new GroupDto();

            // Act
            groupDto.Description = null;
            var actualDescription = groupDto.Description;

            // Assert
            Assert.Null(actualDescription);
        }

        [Fact]
        public void Should_Allow_Null_Members()
        {
            // Arrange
            var groupDto = new GroupDto();

            // Act
            groupDto.Members = null;
            var actualMembers = groupDto.Members;

            // Assert
            Assert.Null(actualMembers);
        }

        [Fact]
        public void Should_Allow_Null_Expenses()
        {
            // Arrange
            var groupDto = new GroupDto();

            // Act
            groupDto.Expenses = null;
            var actualExpenses = groupDto.Expenses;

            // Assert
            Assert.Null(actualExpenses);
        }
    }
}
