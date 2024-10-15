using AutoMapper;
using BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs;
using BacklogItem_MicroService.Models.DTO.DescriptionDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class DescriptionProfile : Profile
    {
        public DescriptionProfile()
        {
            CreateMap<Description, DescriptionDto>()
               .ForMember(dest => dest.DescriptionId, opt => opt.MapFrom(src => src.DescriptionId))
               .ForMember(dest => dest.DescriptionText, opt => opt.MapFrom(src => src.DescriptionText))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItem, opt => opt.MapFrom(src => src.BacklogItem));

            CreateMap<DescriptionCreationDto, Description>()
               .ForMember(dest => dest.DescriptionText, opt => opt.MapFrom(src => src.DescriptionText))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId));

            CreateMap<DescriptionUpdateDto, Description>()
               .ForMember(dest => dest.DescriptionId, opt => opt.MapFrom(src => src.DescriptionId))
               .ForMember(dest => dest.DescriptionText, opt => opt.MapFrom(src => src.DescriptionText));
        }
    }
}
