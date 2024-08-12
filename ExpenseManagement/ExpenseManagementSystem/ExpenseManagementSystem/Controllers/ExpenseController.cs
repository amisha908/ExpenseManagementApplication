using AutoMapper;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddExpense(CreateExpenseDto createExpenseDto)
        {
            var expense = await _expenseService.AddExpenseAsync(createExpenseDto);
            return Ok(expense);
        }

        [HttpPost("add-splits")]
        public async Task<IActionResult> AddExpenseSplits([FromBody] AddExpenseSplitsRequest request)
        {
            if (string.IsNullOrEmpty(request.GroupId))
            {
                return BadRequest("GroupId is required");
            }

            try
            {
                await _expenseService.AddExpenseSplitsAsync(request.GroupId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("group/{groupId}")]
        public async Task<IActionResult> GetExpensesByGroupId(string groupId)
        {
            var expenses = await _expenseService.GetExpensesByGroupIdAsync(groupId);
            if (expenses == null|| expenses.Equals(0))
            {
                return NotFound();
            }
          
            return Ok(expenses);
        }

        [HttpPost]
        [Route("settle")]
        public async Task<IActionResult> SettleExpenses(List<string> expenseIds)
        {
            await _expenseService.SettleExpensesAsync(expenseIds);
            return Ok();
        }
    }
}
