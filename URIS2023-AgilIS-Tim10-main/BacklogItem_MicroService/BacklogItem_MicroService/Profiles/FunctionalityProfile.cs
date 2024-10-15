using AutoMapper;
using BacklogItem_MicroService.Models.DTO.FunctionalityDTOs;
using BacklogItem_MicroService.Models.DTO.StoryPointDTOs;
using BacklogItem_MicroService.Models.DTO.UserStoryDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class FunctionalityProfile: Profile
    {
        public FunctionalityProfile()
        {
            CreateMap<Functionality, FunctionalityDto>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId));

            CreateMap<FunctionalityCreationDto, Functionality>()
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId));

            CreateMap<FunctionalityUpdateDto, Functionality>()
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName));
        }
    }
}
