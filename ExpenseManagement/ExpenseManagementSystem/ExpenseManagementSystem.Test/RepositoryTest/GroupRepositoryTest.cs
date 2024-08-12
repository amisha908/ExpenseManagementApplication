using AutoMapper;
using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test.RepositoryTest
{
    public class GroupRepositoryTests : IDisposable
    {
        private readonly ExpenseSharingContext _context;
        private readonly IMapper _mapper;
        private readonly GroupRepository _groupRepository;

        public GroupRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ExpenseSharingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database for each test
                .Options;
            _context = new ExpenseSharingContext(options);

            var mapperConfig = new MapperConfiguration(cfg => { /* Add mapping profiles here */ });
            _mapper = mapperConfig.CreateMapper();

            _groupRepository = new GroupRepository(_context, _mapper);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnGroup_WhenGroupExists()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var group = new Group { Id = groupId, Name = "Test Group", Description = "Test Description" };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            // Act
            var result = await _groupRepository.GetGroupByIdAsync(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupId, result.Id);
            Assert.Equal(group.Name, result.Name);
            Assert.Equal(group.Description, result.Description);
        }

        [Fact]
        public async Task GetGroupByIdAsync_ShouldReturnNull_WhenGroupDoesNotExist()
        {
            // Act
            var result = await _groupRepository.GetGroupByIdAsync("non-existent-id");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllGroupsAsync_ShouldReturnAllGroups()
        {
            // Arrange
            var groups = new List<Group>
            {
                new Group { Id = Guid.NewGuid().ToString(), Name = "Group 1", Description = "Description 1" },
                new Group { Id = Guid.NewGuid().ToString(), Name = "Group 2", Description = "Description 2" }
            };
            await _context.Groups.AddRangeAsync(groups);
            await _context.SaveChangesAsync();

            // Act
            var result = await _groupRepository.GetAllGroupsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groups.Count, result.Count());
            foreach (var expectedGroup in groups)
            {
                var actualGroup = result.FirstOrDefault(g => g.Id == expectedGroup.Id);
                Assert.NotNull(actualGroup);
                Assert.Equal(expectedGroup.Name, actualGroup.Name);
                Assert.Equal(expectedGroup.Description, actualGroup.Description);
            }
        }

        [Fact]
        public async Task AddGroupAsync_ShouldAddGroupSuccessfully()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var group = new Group { Id = groupId, Name = "New Group", Description = "New Description" };

            // Act
            var result = await _groupRepository.AddGroupAsync(group);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupId, result.Id);
            Assert.Equal(group.Name, result.Name);
            Assert.Equal(group.Description, result.Description);

            // Check database
            var groupFromDb = await _context.Groups.FindAsync(groupId);
            Assert.NotNull(groupFromDb);
            Assert.Equal(group.Name, groupFromDb.Name);
            Assert.Equal(group.Description, groupFromDb.Description);
        }

        [Fact]
        public async Task UpdateGroupAsync_ShouldUpdateGroupSuccessfully()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var group = new Group { Id = groupId, Name = "Original Group", Description = "Original Description" };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            group.Name = "Updated Group";
            group.Description = "Updated Description";

            // Act
            var result = await _groupRepository.UpdateGroupAsync(group);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupId, result.Id);
            Assert.Equal(group.Name, result.Name);
            Assert.Equal(group.Description, result.Description);

            // Check database
            var groupFromDb = await _context.Groups.FindAsync(groupId);
            Assert.NotNull(groupFromDb);
            Assert.Equal(group.Name, groupFromDb.Name);
            Assert.Equal(group.Description, groupFromDb.Description);
        }

        [Fact]
        public async Task DeleteGroupAsync_ShouldDeleteGroupSuccessfully()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var group = new Group { Id = groupId, Name = "Group to Delete", Description = "Description to Delete" };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            // Act
            await _groupRepository.DeleteGroupAsync(groupId);

            // Assert
            var groupFromDb = await _context.Groups.FindAsync(groupId);
            Assert.Null(groupFromDb);
        }

        [Fact]
        public async Task GroupExistsAsync_ShouldReturnTrue_WhenGroupExists()
        {
            // Arrange
            var groupName = "Existing Group";
            var group = new Group { Id = Guid.NewGuid().ToString(), Name = groupName, Description = "Existing Description" };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            // Act
            var result = await _groupRepository.GroupExistsAsync(groupName);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GroupExistsAsync_ShouldReturnFalse_WhenGroupDoesNotExist()
        {
            // Act
            var result = await _groupRepository.GroupExistsAsync("non-existent-name");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var user = new ApplicationUser { Id = userId, UserName = "testuser", Name = "Test User" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _groupRepository.GetUserByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Name, result.Name);
        }
      
        [Fact]
        public async Task DeleteRelatedExpensesAndSplits_ShouldDeleteExpensesAndSplits_WhenGroupHasExpensesAndSplits()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            var group = new Group { Id = groupId, Name = "Test Group", Description = "Test Description" };
            var user = new ApplicationUser { Id = userId, UserName = "testuser", Name = "Test User" };

            var expense1 = new Expense
            {
                Id = Guid.NewGuid().ToString(),
                GroupId = groupId,
                Amount = 100,
                Description = "Expense 1",
                PaidById = userId
            };
            var expense2 = new Expense
            {
                Id = Guid.NewGuid().ToString(),
                GroupId = groupId,
                Amount = 150,
                Description = "Expense 2",
                PaidById = userId
            };
            var split1 = new ExpenseSplit { Id = Guid.NewGuid().ToString(), ExpenseId = expense1.Id, Amount = 50, UserId = userId };
            var split2 = new ExpenseSplit { Id = Guid.NewGuid().ToString(), ExpenseId = expense1.Id, Amount = 50, UserId = userId };
            var split3 = new ExpenseSplit { Id = Guid.NewGuid().ToString(), ExpenseId = expense2.Id, Amount = 75, UserId = userId };

            await _context.Groups.AddAsync(group);
            await _context.Users.AddAsync(user);
            await _context.Expenses.AddRangeAsync(new[] { expense1, expense2 });
            await _context.ExpenseSplits.AddRangeAsync(new[] { split1, split2, split3 });
            await _context.SaveChangesAsync();

            // Act
            await _groupRepository.DeleteRelatedExpensesAndSplitsAsync(groupId);

            // Assert
            var remainingExpenses = await _context.Expenses.Where(e => e.GroupId == groupId).ToListAsync();
            var remainingSplits = await _context.ExpenseSplits.Where(es => es.Expense.GroupId == groupId).ToListAsync();

            Assert.Empty(remainingExpenses);
            Assert.Empty(remainingSplits);
        }

        [Fact]
        public async Task DeleteRelatedExpensesAndSplits_ShouldNotDeleteAnything_WhenGroupHasNoExpenses()
        {
            // Arrange
            var groupId = Guid.NewGuid().ToString();
            var group = new Group { Id = groupId, Name = "Empty Group", Description = "No Expenses" };
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            // Act
            await _groupRepository.DeleteRelatedExpensesAndSplitsAsync(groupId);

            // Assert
            var remainingGroup = await _context.Groups.FindAsync(groupId);
            Assert.NotNull(remainingGroup);
        }

        [Fact]
        public async Task DeleteRelatedExpensesAndSplits_ShouldNotAffectOtherGroups_WhenMultipleGroupsExist()
        {
            // Arrange
            var group1Id = Guid.NewGuid().ToString();
            var group2Id = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();

            var group1 = new Group { Id = group1Id, Name = "Group 1", Description = "Test Group 1" };
            var group2 = new Group { Id = group2Id, Name = "Group 2", Description = "Test Group 2" };
            var user = new ApplicationUser { Id = userId, UserName = "testuser", Name = "Test User" };

            var expense1 = new Expense
            {
                Id = Guid.NewGuid().ToString(),
                GroupId = group1Id,
                Amount = 100,
                Description = "Expense 1",
                PaidById = userId
            };
            var expense2 = new Expense
            {
                Id = Guid.NewGuid().ToString(),
                GroupId = group2Id,
                Amount = 150,
                Description = "Expense 2",
                PaidById = userId
            };

            await _context.Groups.AddRangeAsync(new[] { group1, group2 });
            await _context.Users.AddAsync(user);
            await _context.Expenses.AddRangeAsync(new[] { expense1, expense2 });
            await _context.SaveChangesAsync();

            // Act
            await _groupRepository.DeleteRelatedExpensesAndSplitsAsync(group1Id);

            // Assert
            var remainingExpensesGroup1 = await _context.Expenses.Where(e => e.GroupId == group1Id).ToListAsync();
            var remainingExpensesGroup2 = await _context.Expenses.Where(e => e.GroupId == group2Id).ToListAsync();

            Assert.Empty(remainingExpensesGroup1);
            Assert.Single(remainingExpensesGroup2);
        }


        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var result = await _groupRepository.GetUserByIdAsync("non-existent-id");

            // Assert
            Assert.Null(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Clean up the database after each test
            _context.Dispose();
        }
    }
}
