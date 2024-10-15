using AutoMapper;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class AcceptanceCriteriaConfirmationProfile: Profile
    {
        public AcceptanceCriteriaConfirmationProfile() 
        {
            CreateMap<AcceptanceCriteriaConfirmation, AcceptanceCriteriaConfirmationDto>()
                .ForMember(dest => dest.AcceptanceCriteriaText, opt => opt.MapFrom(src => src.AcceptanceCriteriaText))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItem, opt => opt.MapFrom(src => src.BacklogItem));

            CreateMap<AcceptanceCriteria, AcceptanceCriteriaConfirmation>()
               .ForMember(dest => dest.AcceptanceCriteriaId, opt => opt.MapFrom(src => src.AcceptanceCriteriaId))
               .ForMember(dest => dest.AcceptanceCriteriaText, opt => opt.MapFrom(src => src.AcceptanceCriteriaText))
               .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
               .ForMember(dest => dest.BacklogItem, opt => opt.MapFrom(src => src.BacklogItem));
        }
    }
}
