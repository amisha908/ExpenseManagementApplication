using AutoMapper;
using ExpenseManagement.DAL.Models.DTO;
using ExpenseManagementSystem.ExpenseManagement.DAL;
using ExpenseManagementSystem.Models.Domain;
using System.Linq;

namespace ExpenseManagement.BLL.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Mapping CreateGroupRequest to Group
            CreateMap<CreateGroupRequest, Group>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(dest => dest.Members, opt => opt.Ignore()); // Ignore Members mapping for CreateGroupRequest

            // Mapping Group to CreateGroupResponse
            CreateMap<Group, CreateGroupResponse>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members.Select(m => new MemberDto { Id = m.Id, UserId = m.UserId })))
                .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses.Select(e => new ExpenseDto { Id = e.Id, Amount = e.Amount, Description = e.Description, Date = e.Date })));

            // Mapping Member to MemberDto
            CreateMap<Member, MemberDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name)); // Assuming User has a Name property

            // Mapping Expense to ExpenseDto
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PaidBy.Name))
                .ForMember(dest => dest.IsSettled, opt => opt.MapFrom(src => src.IsSettled))
                .ForMember(dest => dest.ExpenseSplits, opt => opt.MapFrom(src => src.ExpenseSplits.Select(es => new ExpenseSplitDto
                {
                    Id = es.Id,
                    UserId = es.UserId,
                    Amount = es.Amount,
                    Owe = es.Owe,
                    Own = es.Own
                })));

            // Mapping ExpenseSplit to ExpenseSplitDto
            CreateMap<ExpenseSplit, ExpenseSplitDto>();

            // Other mappings
            CreateMap<ExpenseManagementSystem.ExpenseManagement.DAL.Group, ExpenseManagement.DAL.Models.DTO.GroupDto>();
            CreateMap<ExpenseManagement.DAL.Models.DTO.GroupDto, ExpenseManagementSystem.ExpenseManagement.DAL.Group>();
            CreateMap<GroupDto, Group>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members))
                .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses));

            CreateMap<Group, GroupDto>()
               .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.Members.Select(m => new MemberDto
               {
                   Id = m.Id,
                   UserId = m.UserId,
                   GroupId = m.GroupId,
                   Name = m.User.Name // Assuming User has a Name property
               })))
               .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses.Select(e => new ExpenseDto
               {
                   Id = e.Id,
                   Amount = e.Amount,
                   Description = e.Description,
                   Date = e.Date
               })));

            CreateMap<UpdateGroupRequest, Group>()
                .ForMember(dest => dest.Members, opt => opt.Ignore()) // Members will be handled in the service layer
                .ForMember(dest => dest.Expenses, opt => opt.Ignore()); // Ignore Expenses

            CreateMap<CreateExpenseDto, Expense>();
            CreateMap<CreateExpenseSplitDto, ExpenseSplit>();
            CreateMap<Group, GetAllGroupDto>();
            CreateMap<ApplicationUser, UserDto>();
        }
    }
}
