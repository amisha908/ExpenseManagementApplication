using AutoMapper;
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
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupRepository groupRepository, IGroupService groupService, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroup(string id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

      

        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest createGroupRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var group = await _groupService.CreateGroupAsync(createGroupRequestDto);
                var groupDto = _mapper.Map<CreateGroupResponse>(group); // Map entity to DTO
                return Ok(groupDto);
                //var group = await _groupService.CreateGroupAsync(groupDto);
                //return Ok(group);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(string id, [FromBody] UpdateGroupRequest updateGroupRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateGroupRequest.Id)
            {
                return BadRequest("Group ID mismatch");
            }

            try
            {
                var updatedGroup = await _groupService.UpdateGroupAsync(updateGroupRequest);
                return Ok(updatedGroup);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //public async Task<IActionResult> PutGroup(string id, Group group)
        //{
        //    if (id != group.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _groupRepository.UpdateGroupAsync(group);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(string id)
        {
            try
            {
                await _groupService.DeleteGroupAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            // await _groupRepository.DeleteGroupAsync(id);
            // return NoContent();
        }
    }
}
