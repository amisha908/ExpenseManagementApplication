using AutoMapper;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetAllGroupDto>> GetGroupsByUserIdAsync(string userId)
        {
            var groups = await _memberRepository.GetGroupsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<GetAllGroupDto>>(groups);
        }

        public async Task<IEnumerable<MemberDto>> GetMemberByGroupId(string groupId)
        {
            var members = await _memberRepository.GetMemberByGroupId(groupId);
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }
    }
}
