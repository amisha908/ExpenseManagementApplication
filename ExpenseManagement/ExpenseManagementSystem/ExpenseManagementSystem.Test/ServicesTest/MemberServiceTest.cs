using AutoMapper;
using ExpenseManagement.BLL.Services.Implementation;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.ServicesTest
{
    public class MemberServiceTest
    {
        private readonly Mock<IMemberRepository> _mockMemberRepository;
        private readonly IMapper _mapper;
        private readonly MemberService _memberService;

        public MemberServiceTest()
        {
            _mockMemberRepository = new Mock<IMemberRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Group, GetAllGroupDto>();
                cfg.CreateMap<Member, MemberDto>(); // Mapping configuration for Member to MemberDto
            });
            _mapper = config.CreateMapper();

            _memberService = new MemberService(_mockMemberRepository.Object, _mapper);
        }

        [Fact]
        public async Task GetGroupsByUserIdAsync_ReturnsGroups_WhenUserIdIsValid()
        {
            // Arrange
            var userId = "valid_user_id";
            var groups = new List<Group>
            {
                new Group { Id = "1", Name = "Group 1" },
                new Group { Id = "2", Name = "Group 2" }
            };
            _mockMemberRepository.Setup(repo => repo.GetGroupsByUserIdAsync(userId))
                                 .ReturnsAsync(groups);

            // Act
            var result = await _memberService.GetGroupsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Group 1", result.First().Name);
            Assert.Equal("Group 2", result.Last().Name);
        }

        [Fact]
        public async Task GetGroupsByUserIdAsync_ReturnsEmpty_WhenUserIdIsInvalid()
        {
            // Arrange
            var userId = "invalid_user_id";
            _mockMemberRepository.Setup(repo => repo.GetGroupsByUserIdAsync(userId))
                                 .ReturnsAsync(new List<Group>());

            // Act
            var result = await _memberService.GetGroupsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetMemberByGroupId_ReturnsMembers_WhenGroupIdIsValid()
        {
            // Arrange
            var groupId = "valid_group_id";
            var members = new List<Member>
            {
                new Member { Id = "1", UserId = "Member 1" },
                new Member { Id = "2", UserId = "Member 2" }
            };
            _mockMemberRepository.Setup(repo => repo.GetMemberByGroupId(groupId))
                                 .ReturnsAsync(members);

            // Act
            var result = await _memberService.GetMemberByGroupId(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Member 1", result.First().UserId); // Ensure mapping assertion
            Assert.Equal("Member 2", result.Last().UserId); // Ensure mapping assertion
        }

        [Fact]
        public async Task GetMemberByGroupId_ReturnsEmpty_WhenGroupIdIsInvalid()
        {
            // Arrange
            var groupId = "invalid_group_id";
            _mockMemberRepository.Setup(repo => repo.GetMemberByGroupId(groupId))
                                 .ReturnsAsync(new List<Member>());

            // Act
            var result = await _memberService.GetMemberByGroupId(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
