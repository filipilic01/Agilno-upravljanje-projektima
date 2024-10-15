using AutoMapper;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.DTO.StoryPointDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class StoryPointProfile: Profile
    {
        public StoryPointProfile() 
        {
            CreateMap<StoryPoint, StoryPointDto>()
                .ForMember(dest => dest.StoryPointId, opt => opt.MapFrom(src => src.StoryPointId))
                .ForMember(dest => dest.StoryPointValue, opt => opt.MapFrom(src => src.StoryPointValue))
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.BacklogItem, opt => opt.MapFrom(src => src.BacklogItem));

            CreateMap<StoryPointCreationDto, StoryPoint>()
                .ForMember(dest => dest.StoryPointValue, opt => opt.MapFrom(src => src.StoryPointValue))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId));

            CreateMap<StoryPointUpdateDto, StoryPoint>()
               .ForMember(dest => dest.StoryPointId, opt => opt.MapFrom(src => src.StoryPointId))
               .ForMember(dest => dest.StoryPointValue, opt => opt.MapFrom(src => src.StoryPointValue));
        }
    }
}
