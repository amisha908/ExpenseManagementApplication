using AutoMapper;
using ExpenseManagement.BLL.Services.Implementation;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ServicesTest
{
    public class GroupServiceTests
    {
        private readonly Mock<IGroupRepository> _groupRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ExpenseSharingContext _context;
        private readonly GroupService _groupService;

        public GroupServiceTests()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
            _mapperMock = new Mock<IMapper>();
            var options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: "ExpenseSharingTestDb")
                .Options;
            _context = new ExpenseSharingContext(options);
            _groupService = new GroupService(_groupRepositoryMock.Object, _mapperMock.Object, _context);
        }

        [Fact]
        public async Task CreateGroupAsync_ShouldCreateGroupSuccessfully()
        {
            // Arrange
            var createGroupRequest = new CreateGroupRequest
            {
                Name = "New Group",
                Description = "New Group Description",
                MemberIds = new List<string> { "user1", "user2" }
            };

            var groupId = Guid.NewGuid().ToString(); // Generate a consistent group ID

            var group = new Group
            {
                Id = groupId,
                Name = createGroupRequest.Name,
                Description = createGroupRequest.Description
            };

            _mapperMock.Setup(m => m.Map<Group>(It.IsAny<CreateGroupRequest>())).Returns(group);
            _groupRepositoryMock.Setup(r => r.GroupExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _groupRepositoryMock.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = "user1" });
            _groupRepositoryMock.Setup(r => r.AddGroupAsync(It.IsAny<Group>())).ReturnsAsync(group);
            _mapperMock.Setup(m => m.Map<CreateGroupResponse>(It.IsAny<Group>())).Returns(new CreateGroupResponse { Id = groupId });

            // Act
            var result = await _groupService.CreateGroupAsync(createGroupRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupId, result.Id); // Assert with the generated group ID
        }

        // Other test methods (DeleteGroupAsync, UpdateGroupAsync, GetAllGroupsAsync, etc.) remain unchanged.

        [Fact]
        public async Task CreateGroupAsync_ShouldThrowException_WhenGroupExists()
        {
            // Arrange
            var createGroupRequest = new CreateGroupRequest
            {
                Name = "Existing Group",
                Description = "Existing Group Description",
                MemberIds = new List<string> { "user1", "user2" }
            };

            _groupRepositoryMock.Setup(r => r.GroupExistsAsync(createGroupRequest.Name)).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _groupService.CreateGroupAsync(createGroupRequest));
            Assert.Equal("Group already exists.", exception.Message);
        }


        [Fact]
        public async Task CreateGroupAsync_ShouldThrowException_WhenMemberLimitExceeded()
        {
            // Arrange
            var createGroupRequest = new CreateGroupRequest
            {
                Name = "New Group",
                Description = "New Group Description",
                MemberIds = new List<string>
        {
            "user1", "user2", "user3", "user4", "user5",
            "user6", "user7", "user8", "user9", "user10", "user11"
        }
            };

            _mapperMock.Setup(m => m.Map<Group>(It.IsAny<CreateGroupRequest>())).Returns(new Group());
            _groupRepositoryMock.Setup(r => r.GroupExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _groupRepositoryMock.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = "user1" });

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _groupService.CreateGroupAsync(createGroupRequest));
            Assert.Equal("Group cannot have more than 10 members.", exception.Message);
        }



        [Fact]
        public async Task CreateGroupAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var createGroupRequest = new CreateGroupRequest
            {
                Name = "New Group",
                Description = "New Group Description",
                MemberIds = new List<string> { "user1", "user2" }
            };

            _mapperMock.Setup(m => m.Map<Group>(It.IsAny<CreateGroupRequest>())).Returns(new Group());
            _groupRepositoryMock.Setup(r => r.GroupExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _groupRepositoryMock.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _groupService.CreateGroupAsync(createGroupRequest));
        }

        [Fact]
        public async Task DeleteGroupAsync_ShouldDeleteGroupSuccessfully()
        {
            // Arrange
            var groupId = "group1";
            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync(new Group { Id = groupId });

            // Act
            await _groupService.DeleteGroupAsync(groupId);

            // Assert
            _groupRepositoryMock.Verify(r => r.DeleteGroupAsync(groupId), Times.Once);
        }

        [Fact]
        public async Task DeleteGroupAsync_ShouldThrowException_WhenGroupNotFound()
        {
            // Arrange
            var groupId = "group1";
            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync((Group)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _groupService.DeleteGroupAsync(groupId));
        }

        [Fact]
        public async Task GetAllGroupsAsync_ShouldReturnAllGroups()
        {
            // Arrange
            var groups = new List<Group>
    {
        new Group { Id = "group1", Name = "Group 1", Description = "Group 1 Description" },
        new Group { Id = "group2", Name = "Group 2", Description = "Group 2 Description" }
    };

            await _context.Groups.AddRangeAsync(groups);
            await _context.SaveChangesAsync();

            _mapperMock.Setup(m => m.Map<IEnumerable<GroupDto>>(It.IsAny<IEnumerable<Group>>())).Returns(new List<GroupDto>
    {
        new GroupDto { Id = "group1", Name = "Group 1" },
        new GroupDto { Id = "group2", Name = "Group 2" }
    });

            // Act
            var result = await _groupService.GetAllGroupsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }


        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            var groupId = "group1";
            var group = new Group { Id = groupId, Name = "Group 1" };

            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync(group);
            _mapperMock.Setup(m => m.Map<GroupDto>(group)).Returns(new GroupDto { Id = groupId, Name = "Group 1" });

            // Act
            var result = await _groupService.GetGroupByIdAsync(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupId, result.Id);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnNull_WhenGroupDoesNotExist()
        {
            // Arrange
            var groupId = "group1";
            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(groupId)).ReturnsAsync((Group)null);

            // Act
            var result = await _groupService.GetGroupByIdAsync(groupId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateGroupAsync_ShouldUpdateGroupSuccessfully()
        {
            // Arrange
            var updateGroupRequest = new UpdateGroupRequest
            {
                Id = "group1",
                Name = "Updated Group",
                Description = "Updated Description",
                MemberIds = new List<string> { "user1", "user2" }
            };

            var group = new Group { Id = updateGroupRequest.Id, Name = "Group 1", Description = "Description" };

            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(updateGroupRequest.Id)).ReturnsAsync(group);
            _groupRepositoryMock.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = "user1" });
            _groupRepositoryMock.Setup(r => r.UpdateGroupAsync(It.IsAny<Group>())).ReturnsAsync((Group g) =>
            {
                g.Name = updateGroupRequest.Name;
                g.Description = updateGroupRequest.Description;
                return g;
            });

            _mapperMock.Setup(m => m.Map<GroupDto>(It.IsAny<Group>())).Returns((Group src) => new GroupDto { Id = src.Id, Name = src.Name });

            // Act
            var result = await _groupService.UpdateGroupAsync(updateGroupRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updateGroupRequest.Name, result.Name);
        }


        [Fact]
        public async Task UpdateGroupAsync_ShouldThrowException_WhenGroupNotFound()
        {
            // Arrange
            var updateGroupRequest = new UpdateGroupRequest
            {
                Id = "group1",
                Name = "Updated Group",
                Description = "Updated Description",
                MemberIds = new List<string> { "user1", "user2" }
            };

            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(updateGroupRequest.Id)).ReturnsAsync((Group)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _groupService.UpdateGroupAsync(updateGroupRequest));
        }

        [Fact]
        public async Task UpdateGroupAsync_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var updateGroupRequest = new UpdateGroupRequest
            {
                Id = "group1",
                Name = "Updated Group",
                Description = "Updated Description",
                MemberIds = new List<string> { "user1", "user2" }
            };

            var group = new Group { Id = updateGroupRequest.Id, Name = "Group 1", Description = "Description" };

            _groupRepositoryMock.Setup(r => r.GetGroupByIdAsync(updateGroupRequest.Id)).ReturnsAsync(group);
            _groupRepositoryMock.Setup(r => r.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _groupService.UpdateGroupAsync(updateGroupRequest));
        }
    }
}
