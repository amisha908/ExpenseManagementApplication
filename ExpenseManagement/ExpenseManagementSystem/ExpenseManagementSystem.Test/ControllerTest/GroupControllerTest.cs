using AutoMapper;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.Controllers;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ControllerTest
{
    public class GroupControllerTests
    {
        private readonly Mock<IGroupRepository> _mockGroupRepository;
        private readonly Mock<IGroupService> _mockGroupService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GroupController _groupController;

        public GroupControllerTests()
        {
            _mockGroupRepository = new Mock<IGroupRepository>();
            _mockGroupService = new Mock<IGroupService>();
            _mockMapper = new Mock<IMapper>();
            _groupController = new GroupController(_mockGroupRepository.Object, _mockGroupService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetGroups_ShouldReturnOkWithGroups()
        {
            // Arrange
            var groups = new List<GroupDto>
            {
                new GroupDto { Id = "1", Name = "Group1" },
                new GroupDto { Id = "2", Name = "Group2" }
            };
            _mockGroupService.Setup(service => service.GetAllGroupsAsync()).ReturnsAsync(groups);

            // Act
            var result = await _groupController.GetGroups();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GroupDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetGroup_ShouldReturnOkWithGroup()
        {
            // Arrange
            var group = new GroupDto { Id = "1", Name = "Group1" };
            _mockGroupService.Setup(service => service.GetGroupByIdAsync("1")).ReturnsAsync(group);

            // Act
            var result = await _groupController.GetGroup("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<GroupDto>(okResult.Value);
            Assert.Equal("1", returnValue.Id);
        }

        [Fact]
        public async Task GetGroup_ShouldReturnNotFound()
        {
            // Arrange
            _mockGroupService.Setup(service => service.GetGroupByIdAsync("1")).ReturnsAsync((GroupDto)null);

            // Act
            var result = await _groupController.GetGroup("1");

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateGroup_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            var createGroupRequest = new CreateGroupRequest
            {
                Name = "Test Group",
                Description = "This is a test group"
            };

            var createdGroup = new CreateGroupResponse
            {
                Id = "group1",
                Name = createGroupRequest.Name,
                Description = createGroupRequest.Description
            };

            _mockGroupService.Setup(x => x.CreateGroupAsync(It.IsAny<CreateGroupRequest>()))
                            .ReturnsAsync(createdGroup);

            _mockMapper.Setup(m => m.Map<CreateGroupResponse>(It.IsAny<object>()))
                      .Returns(createdGroup); // Mock mapping

            // Act
            var result = await _groupController.CreateGroup(createGroupRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var groupDto = Assert.IsType<CreateGroupResponse>(okResult.Value);
            Assert.Equal("group1", groupDto.Id);
            Assert.Equal(createGroupRequest.Name, groupDto.Name);
            Assert.Equal(createGroupRequest.Description, groupDto.Description);
        }

        [Fact]
        public async Task CreateGroup_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _groupController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _groupController.CreateGroup(new CreateGroupRequest());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task PutGroup_ShouldReturnOkWithUpdatedGroup()
        {
            // Arrange
            var updateGroupRequest = new UpdateGroupRequest { Id = "1", Name = "Updated Group" };
            var updatedGroup = new GroupDto { Id = "1", Name = "Updated Group" };

            _mockGroupService.Setup(service => service.UpdateGroupAsync(updateGroupRequest)).ReturnsAsync(updatedGroup);

            // Act
            var result = await _groupController.PutGroup("1", updateGroupRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GroupDto>(okResult.Value);
            Assert.Equal("1", returnValue.Id);
        }

        //[Fact]
        //public async Task PutGroup_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        //{
        //    // Arrange
        //    _groupController.ModelState.AddModelError("Name", "Required");

        //    // Act
        //    var result = await _groupController.PutGroup("1", new UpdateGroupRequest());

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.IsType<SerializableError>(badRequestResult.Value);
        //}

        [Fact]
        public async Task PutGroup_ShouldReturnBadRequest_WhenIdMismatch()
        {
            // Arrange
            var updateGroupRequest = new UpdateGroupRequest { Id = "2", Name = "Updated Group" };

            // Act
            var result = await _groupController.PutGroup("1", updateGroupRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Group ID mismatch", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteGroup_ShouldReturnNoContent()
        {
            // Arrange
            _mockGroupService.Setup(service => service.DeleteGroupAsync("1")).Returns(Task.CompletedTask);

            // Act
            var result = await _groupController.DeleteGroup("1");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteGroup_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            _mockGroupService.Setup(service => service.DeleteGroupAsync("1")).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _groupController.DeleteGroup("1");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }
        [Fact]
        public async Task CreateGroup_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var createGroupRequest = new CreateGroupRequest
            {
                Name = "Test Group",
                Description = "This is a test group"
            };

            _mockGroupService.Setup(x => x.CreateGroupAsync(It.IsAny<CreateGroupRequest>()))
                            .ThrowsAsync(new Exception("Error creating group"));

            // Act
            var result = await _groupController.CreateGroup(createGroupRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error creating group", badRequestResult.Value);
        }
        [Fact]
        public async Task PutGroup_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _groupController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _groupController.PutGroup("1", new UpdateGroupRequest());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
        [Fact]
        public async Task PutGroup_ShouldReturnBadRequest_OnException()
        {
            // Arrange
            var updateGroupRequest = new UpdateGroupRequest { Id = "1", Name = "Updated Group" };

            _mockGroupService.Setup(service => service.UpdateGroupAsync(updateGroupRequest)).ThrowsAsync(new Exception("Error updating group"));

            // Act
            var result = await _groupController.PutGroup("1", updateGroupRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error updating group", badRequestResult.Value);
        }
    }
}
