using ExpenseManagementSystem.ExpenseManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Interface
{
    public interface IMemberRepository
    {
        //Task<Member> GetMemberByIdAsync(int id);
        //Task<IEnumerable<Member>> GetAllMembersAsync();
        //Task<Member> AddMemberAsync(Member member);
        //Task<Member> UpdateMemberAsync(Member member);
        //Task DeleteMemberAsync(int id);
        Task<IEnumerable<Group>> GetGroupsByUserIdAsync(string userId);
        Task<IEnumerable<Member>> GetMemberByGroupId(string groupId);
    }
}
