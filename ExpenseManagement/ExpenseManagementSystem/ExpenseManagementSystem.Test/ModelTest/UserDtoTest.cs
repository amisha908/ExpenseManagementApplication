using ExpenseManagement.DAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test.ModelTest
{
    public class UserDtoTest
    {
        [Fact]
        public void CreateUserDto_WithValidData_ReturnsDtoInstance()
        {
            // Arrange
            var dto = new UserDto
            {
                Id = "1",
                Email = "test@example.com",
                Name = "John Doe",
                TotalOwes = 100.00m,
                TotalOwns = 50.00m
            };

            // Act & Assert
            Assert.Equal("1", dto.Id);
            Assert.Equal("test@example.com", dto.Email);
            Assert.Equal("John Doe", dto.Name);
            Assert.Equal(100.00m, dto.TotalOwes);
            Assert.Equal(50.00m, dto.TotalOwns);
        }

        [Fact]
        public void CreateEmptyUserDto_ReturnsDtoInstanceWithDefaultValues()
        {
            // Arrange
            var dto = new UserDto();

            // Act & Assert
            Assert.Null(dto.Id);
            Assert.Null(dto.Email);
            Assert.Null(dto.Name);
            Assert.Equal(0m, dto.TotalOwes);
            Assert.Equal(0m, dto.TotalOwns);
        }

       

      
    }
}
