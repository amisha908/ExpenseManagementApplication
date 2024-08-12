using ExpenseManagementSystem.ExpenseManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class GroupModelTest
    {
        [Fact]
        public void Group_CanSetAndGetId()
        {
            // Arrange
            var group = new Group();
            var id = "test-id";

            // Act
            group.Id = id;

            // Assert
            Assert.Equal(id, group.Id);
        }

        [Fact]
        public void Group_CanSetAndGetName()
        {
            // Arrange
            var group = new Group();
            var name = "Test Group";

            // Act
            group.Name = name;

            // Assert
            Assert.Equal(name, group.Name);
        }

        [Fact]
        public void Group_CanSetAndGetDescription()
        {
            // Arrange
            var group = new Group();
            var description = "Test description.";

            // Act
            group.Description = description;

            // Assert
            Assert.Equal(description, group.Description);
        }

        [Fact]
        public void Group_CanSetAndGetCreatedDate()
        {
            // Arrange
            var group = new Group();
            var createdDate = DateTime.Now;

            // Act
            group.CreatedDate = createdDate;

            // Assert
            Assert.Equal(createdDate, group.CreatedDate);
        }

        [Fact]
        public void Group_MembersCollectionInitialized()
        {
            // Arrange
            var group = new Group();

            // Assert
            Assert.NotNull(group.Members);
            Assert.IsAssignableFrom<ICollection<Member>>(group.Members);
        }

        [Fact]
        public void Group_ExpensesCollectionInitialized()
        {
            // Arrange
            var group = new Group();

            // Assert
            Assert.NotNull(group.Expenses);
            Assert.IsAssignableFrom<ICollection<Expense>>(group.Expenses);
        }
    }
}
