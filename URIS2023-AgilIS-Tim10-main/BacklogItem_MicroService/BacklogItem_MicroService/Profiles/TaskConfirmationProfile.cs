using AutoMapper;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.TaskDTOs;
using BacklogItem_MicroService.Models.DTO.UserStoryDTOs;
using BacklogItem_MicroService.Models.Entities;

namespace BacklogItem_MicroService.Profiles
{
    public class TaskConfirmationProfile: Profile
    {
        public TaskConfirmationProfile()
        {
            CreateMap<TaskConfirmation, TaskConfirmationDto>()
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.BacklogId, opt => opt.MapFrom(src => src.BacklogId));


            CreateMap<TaskEntity, TaskConfirmation>()
                .ForMember(dest => dest.BacklogItemId, opt => opt.MapFrom(src => src.BacklogItemId))
                .ForMember(dest => dest.BacklogItemName, opt => opt.MapFrom(src => src.BacklogItemName));
        }
    }
}
