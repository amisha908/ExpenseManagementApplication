using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMemberService _memberService;

        public MemberController(IMemberRepository memberRepository, IMemberService memberService)
        {
            _memberRepository = memberRepository;
            _memberService = memberService;
        }

        

        [HttpGet("groups/{userId}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroupsByUserId(string userId)
        {
            var groupDtos = await _memberService.GetGroupsByUserIdAsync(userId);
            if (groupDtos == null)
            {
                return NotFound();
            }
            return Ok(groupDtos);


        }
        [HttpGet("member/{groupId}")]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetMemberByGroupId(string groupId)
        {
            var memberDtos = await _memberService.GetMemberByGroupId(groupId);
            if (memberDtos == null)
            {
                return NotFound();
            }
            return Ok(memberDtos);


        }
    }
}