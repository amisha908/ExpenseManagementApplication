using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<UserDto> users = await _userService.GetAllUsersAsync();
            if(users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        [HttpGet("GetUserTotalOwes/{id}")]
        public async Task<IActionResult> GetUserTotalOwes(string id)
        {
            var totalOwes = await _userService.GetUserTotalOwesAsync(id);
            return Ok(totalOwes);
        }

        [HttpGet("GetUserTotalOwns/{id}")]
        public async Task<IActionResult> GetUserTotalOwns(string id)
        {
            var totalOwns = await _userService.GetUserTotalOwnsAsync(id);
            return Ok(totalOwns);
        }
    }
}
