using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class MemberDtoTest
    {
        [Fact]
        public void Should_SetAndGet_Id()
        {
            // Arrange
            var memberDto = new MemberDto();
            var expectedId = "mem123";

            // Act
            memberDto.Id = expectedId;
            var actualId = memberDto.Id;

            // Assert
            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void Should_SetAndGet_UserId()
        {
            // Arrange
            var memberDto = new MemberDto();
            var expectedUserId = "user456";

            // Act
            memberDto.UserId = expectedUserId;
            var actualUserId = memberDto.UserId;

            // Assert
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public void Should_SetAndGet_GroupId()
        {
            // Arrange
            var memberDto = new MemberDto();
            var expectedGroupId = "grp789";

            // Act
            memberDto.GroupId = expectedGroupId;
            var actualGroupId = memberDto.GroupId;

            // Assert
            Assert.Equal(expectedGroupId, actualGroupId);
        }

        [Fact]
        public void Should_SetAndGet_Name()
        {
            // Arrange
            var memberDto = new MemberDto();
            var expectedName = "John";

            // Act
            memberDto.Name = expectedName;
            var actualName = memberDto.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void Should_Allow_Null_Id()
        {
            // Arrange
            var memberDto = new MemberDto();

            // Act
            memberDto.Id = null;
            var actualId = memberDto.Id;

            // Assert
            Assert.Null(actualId);
        }

        [Fact]
        public void Should_Allow_Null_UserId()
        {
            // Arrange
            var memberDto = new MemberDto();

            // Act
            memberDto.UserId = null;
            var actualUserId = memberDto.UserId;

            // Assert
            Assert.Null(actualUserId);
        }

        [Fact]
        public void Should_Allow_Null_GroupId()
        {
            // Arrange
            var memberDto = new MemberDto();

            // Act
            memberDto.GroupId = null;
            var actualGroupId = memberDto.GroupId;

            // Assert
            Assert.Null(actualGroupId);
        }

        [Fact]
        public void Should_Allow_Null_Name()
        {
            // Arrange
            var memberDto = new MemberDto();

            // Act
            memberDto.Name = null;
            var actualName = memberDto.Name;

            // Assert
            Assert.Null(actualName);
        }
    }
}
