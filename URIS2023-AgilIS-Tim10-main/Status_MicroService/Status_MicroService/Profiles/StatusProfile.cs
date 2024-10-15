using AutoMapper;
using Status_MicroService.Entities;
using Status_MicroService.Model;

namespace Status_MicroService.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile() 
        {
            CreateMap<Status, StatusDto>().ForMember(dest => dest.StatusValue, opt => opt.MapFrom(src => src.VrednostStatusa));
            CreateMap<StatusCreationDto, Status>();
            CreateMap<StatusUpdateDto, Status>();
            CreateMap<StatusUpdateDto, StatusDto>();
            CreateMap<Status, StatusConfirmation>();
            CreateMap<StatusUpdateDto, StatusConfirmationDto>();
            CreateMap<StatusConfirmation, StatusConfirmationDto>();
        }
    }
}
