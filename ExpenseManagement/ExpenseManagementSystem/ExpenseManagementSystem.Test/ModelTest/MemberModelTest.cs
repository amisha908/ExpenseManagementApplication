using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class MemberModelTest
    {
        [Fact]
        public void Member_CanSetAndGetId()
        {
            // Arrange
            var member = new Member();
            var id = "test-id";

            // Act
            member.Id = id;

            // Assert
            Assert.Equal(id, member.Id);
        }

        [Fact]
        public void Member_CanSetAndGetUserId()
        {
            // Arrange
            var member = new Member();
            var userId = "test-user-id";

            // Act
            member.UserId = userId;

            // Assert
            Assert.Equal(userId, member.UserId);
        }

        [Fact]
        public void Member_CanSetAndGetUser()
        {
            // Arrange
            var member = new Member();
            var user = new ApplicationUser { Id = "user-id", UserName = "test-user" };

            // Act
            member.User = user;

            // Assert
            Assert.Equal(user, member.User);
        }

        [Fact]
        public void Member_CanSetAndGetGroupId()
        {
            // Arrange
            var member = new Member();
            var groupId = "test-group-id";

            // Act
            member.GroupId = groupId;

            // Assert
            Assert.Equal(groupId, member.GroupId);
        }

        [Fact]
        public void Member_CanSetAndGetGroup()
        {
            // Arrange
            var member = new Member();
            var group = new Group { Id = "group-id", Name = "test-group" };

            // Act
            member.Group = group;

            // Assert
            Assert.Equal(group, member.Group);
        }

        [Fact]
        public void Member_CanHandleNullUser()
        {
            // Arrange
            var member = new Member();

            // Act
            member.User = null;

            // Assert
            Assert.Null(member.User);
        }

        [Fact]
        public void Member_CanHandleNullGroup()
        {
            // Arrange
            var member = new Member();

            // Act
            member.Group = null;

            // Assert
            Assert.Null(member.Group);
        }
    }
}
