using AutoMapper;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.DTO.EpicDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class EpicConfirmationProfile: Profile
    {
        public EpicConfirmationProfile() 
        {
            CreateMap<EpicConfirmation, EpicConfirmationDto>()
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId));


            CreateMap<Epic, EpicConfirmation>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName));
        }
    }
}
