using AutoMapper;
using DataBaseManager.Models.AuthModels;
using DataBaseManager.Dtos.User;

namespace OpenAiService.Mapper;


public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Mapping from Create DTO to Entity
        CreateMap<UserCreateRequestDto, User>()
            .ForMember(dest => dest.LockoutEnabled, opt => opt.MapFrom(src => true));

        // Mapping from Update DTO to Entity
        CreateMap<UserUpdateRequestDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.ProfileImageName, opt => opt.MapFrom(src => src.ProfileImageName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
    }
}
