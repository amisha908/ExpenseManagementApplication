using AutoMapper;
using ExpenseManagement.BLL.Services.Interface;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagement.DAL.Repository.Implementation;
using ExpenseManagement.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.BLL.Services.Implementation
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IExpenseSplitRepository _expenseSplitRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IExpenseSplitRepository expenseSplitRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _expenseSplitRepository = expenseSplitRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<decimal> GetUserTotalOwesAsync(string userId)
        {
            var expenseSplits = await _expenseSplitRepository.GetExpenseSplitsByUserIdAsync(userId);
            return expenseSplits.Sum(es => es.Owe);
        }

        public async Task<decimal> GetUserTotalOwnsAsync(string userId)
        {
            var expenseSplits = await _expenseSplitRepository.GetExpenseSplitsByUserIdAsync(userId);
            return expenseSplits.Sum(es => es.Own);
        }

    }
}
