using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ControllerTest
{
    public class MemberControllerTests
    {
        private readonly Mock<IMemberRepository> _mockMemberRepository;
        private readonly Mock<IMemberService> _mockMemberService;
        private readonly MemberController _controller;

        public MemberControllerTests()
        {
            _mockMemberRepository = new Mock<IMemberRepository>();
            _mockMemberService = new Mock<IMemberService>();
            _controller = new MemberController(_mockMemberRepository.Object, _mockMemberService.Object);
        }

        [Fact]
        public async Task GetGroupsByUserId_ReturnsOkResult_WithAListOfGroups()
        {
            // Arrange
            var userId = "test-user-id";
            var groupDtos = new List<GetAllGroupDto>
            {
                new GetAllGroupDto { Id = "1", Name = "Group 1" },
                new GetAllGroupDto { Id = "2", Name = "Group 2" }
            };
            _mockMemberService.Setup(service => service.GetGroupsByUserIdAsync(userId))
                .ReturnsAsync(groupDtos);

            // Act
            var result = await _controller.GetGroupsByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GetAllGroupDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetGroupsByUserId_ReturnsOkResult_WithEmptyList_WhenNoGroupsFound()
        {
            // Arrange
            var userId = "test-user-id";
            _mockMemberService.Setup(service => service.GetGroupsByUserIdAsync(userId))
                .ReturnsAsync(new List<GetAllGroupDto>());

            // Act
            var result = await _controller.GetGroupsByUserId(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<GetAllGroupDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetGroupsByUserId_ReturnsNotFoundResult_WhenUserIdIsInvalid()
        {
            // Arrange
            var userId = "invalid-user-id";
            _mockMemberService.Setup(service => service.GetGroupsByUserIdAsync(userId))
                .ReturnsAsync((IEnumerable<GetAllGroupDto>)null);

            // Act
            var result = await _controller.GetGroupsByUserId(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public async Task GetMemberByGroupId_ReturnsOkResult_WithAListOfMembers()
        {
            // Arrange
            var groupId = "test-group-id";
            var memberDtos = new List<MemberDto>
            {
                new MemberDto { Id = "1", Name = "Member 1" },
                new MemberDto { Id = "2", Name = "Member 2" }
            };
            _mockMemberService.Setup(service => service.GetMemberByGroupId(groupId))
                .ReturnsAsync(memberDtos);

            // Act
            var result = await _controller.GetMemberByGroupId(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<MemberDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetMemberByGroupId_ReturnsOkResult_WithEmptyList_WhenNoMembersFound()
        {
            // Arrange
            var groupId = "test-group-id";
            _mockMemberService.Setup(service => service.GetMemberByGroupId(groupId))
                .ReturnsAsync(new List<MemberDto>());

            // Act
            var result = await _controller.GetMemberByGroupId(groupId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<MemberDto>>(okResult.Value);
            Assert.Empty(returnValue);
        }

        [Fact]
        public async Task GetMemberByGroupId_ReturnsNotFoundResult_WhenGroupIdIsInvalid()
        {
            // Arrange
            var groupId = "invalid-group-id";
            _mockMemberService.Setup(service => service.GetMemberByGroupId(groupId))
                .ReturnsAsync((IEnumerable<MemberDto>)null);

            // Act
            var result = await _controller.GetMemberByGroupId(groupId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }


    }
}
