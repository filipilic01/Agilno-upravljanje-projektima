using AutoMapper;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs;
using BacklogItem_MicroService.Models.DTO.StoryPointDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class StoryPointConfirmationProfile : Profile
    {
        public StoryPointConfirmationProfile() 
        {
            CreateMap<StoryPointConfirmation, StoryPointConfirmationDto>()
                .ForMember(dest => dest.StoryPointValue, opt => opt.MapFrom(src => src.StoryPointValue))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItem, opt => opt.MapFrom(src => src.BacklogItem));

            CreateMap<StoryPoint, StoryPointConfirmation>()
               .ForMember(dest => dest.StoryPointId, opt => opt.MapFrom(src => src.StoryPointId))
               .ForMember(dest => dest.StoryPointValue, opt => opt.MapFrom(src => src.StoryPointValue))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItem, opt => opt.MapFrom(src => src.BacklogItem));


        }
    }
}
