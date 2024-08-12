using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.RepositoryTest
{
    public class MemberRepositoryTest : IDisposable
    {
        private readonly ExpenseSharingContext _context;
        private readonly MemberRepository _memberRepository;

        public MemberRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                .Options;
            _context = new ExpenseSharingContext(options);
            _memberRepository = new MemberRepository(_context);
        }

        [Fact]
        public async Task GetGroupsByUserIdAsync_ReturnsGroups_ForValidUserId()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            // Arrange
            var userId = "valid-user-id";
            var group1 = new Group { Id = "1", Name = "Group 1", Description = "Description 1" };
            var group2 = new Group { Id = "2", Name = "Group 2", Description = "Description 2" };

            _context.Groups.AddRange(group1, group2);

            var member1 = new Member { Id = "1", UserId = userId, GroupId = "1", Group = group1 };
            var member2 = new Member { Id = "2", UserId = userId, GroupId = "2", Group = group2 };
            _context.Members.AddRange(member1, member2);

            await _context.SaveChangesAsync();

            // Act
            var result = await _memberRepository.GetGroupsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, g => g.Id == "1");
            Assert.Contains(result, g => g.Id == "2");
        }

        [Fact]
        public async Task GetGroupsByUserIdAsync_ReturnsEmptyList_ForInvalidUserId()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            // Arrange
            var userId = "invalid-user-id";

            // Act
            var result = await _memberRepository.GetGroupsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetMemberByGroupId_ReturnsMembers_ForValidGroupId()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            // Arrange
            var groupId = "valid-group-id";
            var user1 = new ApplicationUser { Id = "user1", UserName = "User 1", Name = "User One" }; // Ensure Name is set
            var user2 = new ApplicationUser { Id = "user2", UserName = "User 2", Name = "User Two" }; // Ensure Name is set

            var member1 = new Member { Id = "1", GroupId = groupId, UserId = "user1", User = user1 };
            var member2 = new Member { Id = "2", GroupId = groupId, UserId = "user2", User = user2 };

            _context.Users.AddRange(user1, user2);
            _context.Members.AddRange(member1, member2);

            await _context.SaveChangesAsync();

            // Act
            var result = await _memberRepository.GetMemberByGroupId(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, m => m.UserId == "user1");
            Assert.Contains(result, m => m.UserId == "user2");
        }


        [Fact]
        public async Task GetMemberByGroupId_ReturnsEmptyList_ForInvalidGroupId()
        {
            // Arrange
            var groupId = "invalid-group-id";

            // Act
            var result = await _memberRepository.GetMemberByGroupId(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetMemberByGroupId_ReturnsMembers_WithUserDetails()
        {
            // Arrange
            var groupId = "valid-group-id";
            var user = new ApplicationUser { Id = "user1", UserName = "User 1", Name = "User One" }; // Ensure Name is set
            var member = new Member { Id = "1", GroupId = groupId, UserId = "user1", User = user };

            _context.Users.Add(user);
            _context.Members.Add(member);

            await _context.SaveChangesAsync();

            // Act
            var result = await _memberRepository.GetMemberByGroupId(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("user1", result.First().UserId);
            Assert.Equal("User 1", result.First().User.UserName);
            Assert.Equal("User One", result.First().User.Name); // Check Name
        }

        [Fact]
        public async Task GetMemberByGroupId_ThrowsException_ForMissingUserDetails()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            var groupId = "valid-group-id";
            var member = new Member { Id = "1", GroupId = groupId, UserId = "missing-user" };

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _memberRepository.GetMemberByGroupId(groupId));
            Assert.Contains("User details are missing for one or more members.", exception.Message);
        }

        [Fact]
        public async Task GetGroupsByUserIdAsync_ThrowsException_ForGeneralError()
        {
            // Arrange
            var userId = "user-id";

            // Simulate an error by disposing the context
            _context.Dispose();

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _memberRepository.GetGroupsByUserIdAsync(userId));
            Assert.Contains("An error occurred while fetching groups for the user.", ex.Message);
        }
        




        public void Dispose()
        {
            _context.Dispose();
        }

    }
}



