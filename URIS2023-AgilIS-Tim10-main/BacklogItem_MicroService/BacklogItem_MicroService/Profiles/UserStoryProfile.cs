using AutoMapper;
using BacklogItem_MicroService.Models.DTO.EpicDTOs;
using BacklogItem_MicroService.Models.DTO.UserStoryDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class UserStoryProfile: Profile
    {
        public UserStoryProfile()
        {
            CreateMap<UserStory, UserStoryDto>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId));

            CreateMap<UserStoryCreationDto, UserStory>()
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId));

            CreateMap<UserStoryUpdateDto, UserStory>()
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName));
        }
    }
}
