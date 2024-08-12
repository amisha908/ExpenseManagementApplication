using AutoMapper;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace ExpenseManagement.BLL.Services.Implementation
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly ExpenseSharingContext _context;

        public GroupService(IGroupRepository groupRepository, IMapper mapper, ExpenseSharingContext context)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _context = context;
        }

        public ExpenseSharingContext Context { get; }

        public async Task<CreateGroupResponse> CreateGroupAsync(CreateGroupRequest groupDto)
        {
            if (await _groupRepository.GroupExistsAsync(groupDto.Name))
            {
                throw new Exception("Group already exists.");
            }

            if (groupDto.MemberIds.Count > 10)
            {
                throw new Exception("Group cannot have more than 10 members.");
            }

            var group = _mapper.Map<Group>(groupDto);
            group.Id = Guid.NewGuid().ToString();

            foreach (var memberId in groupDto.MemberIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(memberId);
                if (user == null)
                {
                    throw new Exception($"User with ID {memberId} not found.");
                }

                var member = new Member
                {
                    Id = Guid.NewGuid().ToString(),
                    GroupId = group.Id,
                    UserId = user.Id
                };

                group.Members.Add(member);
            }

            await _groupRepository.AddGroupAsync(group);
            var response = _mapper.Map<CreateGroupResponse>(group);

            return response;
        }


        public async Task DeleteGroupAsync(string id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            if (group == null)
            {
                throw new Exception($"Group with ID {id} not found.");
            }

            // Delete related expenses and expense splits
            await _groupRepository.DeleteRelatedExpensesAndSplitsAsync(group.Id);

            // Now delete the group
            await _groupRepository.DeleteGroupAsync(id);
            //var group = await _groupRepository.GetGroupByIdAsync(id);
            //if (group == null)
            //{
            //    throw new Exception($"Group with ID {id} not found.");
            //}

            //await _groupRepository.DeleteGroupAsync(id);
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroupsAsync()
        {
            var groups = await _context.Groups
                .Include(g => g.Members)
                .ThenInclude(m => m.User) // Assuming Member has a User property
                .Include(g => g.Expenses)
                .ToListAsync();
            return _mapper.Map<IEnumerable<GroupDto>>(groups);
        }


        public async Task<GroupDto> GetGroupByIdAsync(string groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                return null;
            }

            var groupDto = _mapper.Map<GroupDto>(group);
            return groupDto;
           

        }

        public async Task<GroupDto> UpdateGroupAsync(UpdateGroupRequest updateGroupRequest)
        {
            var group = await _groupRepository.GetGroupByIdAsync(updateGroupRequest.Id);
            if (group == null)
            {
                throw new Exception("Group not found");
            }

            group.Name = updateGroupRequest.Name;
            group.Description = updateGroupRequest.Description;

            // Update members
            group.Members.Clear();
            foreach (var memberId in updateGroupRequest.MemberIds)
            {
                var user = await _groupRepository.GetUserByIdAsync(memberId);
                if (user != null)
                {
                    group.Members.Add(new Member
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = user.Id,
                        GroupId = group.Id,
                        Group = group
                    });
                }
                else
                {
                    throw new Exception($"User with ID {memberId} not found.");
                }
            }

            var updatedGroup = await _groupRepository.UpdateGroupAsync(group);
            return _mapper.Map<GroupDto>(updatedGroup);
        }

      
    }
}

