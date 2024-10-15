using AutoMapper;
using User_Service.Entities;
using User_Service.Models;
using User_Service.Models.User;

namespace User_Service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
            CreateMap<UserCreationDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<UserUpdateDto, UserDto>();
            CreateMap<User, UserConfirmation>();
            CreateMap<UserUpdateDto, UserConfirmationDto>();
            CreateMap<UserConfirmation, UserConfirmationDto>();

        }
       
    }
}
