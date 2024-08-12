using ExpenseManagement.DAL.Repository.Interface;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.DAL.Repository.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ExpenseSharingContext _context;

        public MemberRepository(ExpenseSharingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroupsByUserIdAsync(string userId)
        {
            try
            {
                // Query groups associated with the user
                var groups = await _context.Members
                    .Where(m => m.UserId == userId && m.Group != null) // Ensure there is a related group
                    .Select(m => m.Group)
                    .ToListAsync();

                // Ensure that all groups have required properties
                foreach (var group in groups)
                {
                    if (string.IsNullOrEmpty(group.Description)) // Example condition, adjust as per your required property checks
                    {
                        throw new DbUpdateException($"Required properties are missing for the instance of entity type 'Group'.");
                    }
                }

                return groups;
            }
            catch (DbUpdateException)
            {
                throw; // Rethrow DbUpdateException to maintain consistency
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching groups for the user.", ex);
            }
        }

        public async Task<IEnumerable<Member>> GetMemberByGroupId(string groupId)
        {
           
                var members = await _context.Members
                                    .Where(m => m.GroupId == groupId)
                                    .ToListAsync();

                // Explicitly load User for each member
                foreach (var member in members)
                {
                    await _context.Entry(member).Reference(m => m.User).LoadAsync();
                }

                if (members.Any(m => m.User == null))
                {
                    throw new InvalidOperationException("User details are missing for one or more members.");
                }

                return members;
            }
            //return await _context.Members
            //    .Where(m => m.GroupId == groupId)
            //    .Include(m => m.User)
            //    .ToListAsync();
        
    }
}

