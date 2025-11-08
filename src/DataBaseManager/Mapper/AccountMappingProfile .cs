using AutoMapper;
using BineshSoloution.Dtos.Account;
using BineshSoloution.Models.Account;

namespace BineshSoloution.Mapper;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.SumDebit, opt => opt.MapFrom(src => src.SumDebit))
            .ForMember(dest => dest.SumCredit, opt => opt.MapFrom(src => src.SumCredit))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.Debit))
            .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.Credit))
            .ForMember(dest => dest.SubAccounts, opt => opt.MapFrom(src => src.SubAccounts))
            .ReverseMap()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))
            .ForMember(dest => dest.SumDebit, opt => opt.MapFrom(src => src.SumDebit))
            .ForMember(dest => dest.SumCredit, opt => opt.MapFrom(src => src.SumCredit))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Debit, opt => opt.MapFrom(src => src.Debit))
            .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.Credit))
            .ForMember(dest => dest.SubAccounts, opt => opt.MapFrom(src => src.SubAccounts));
    }
}