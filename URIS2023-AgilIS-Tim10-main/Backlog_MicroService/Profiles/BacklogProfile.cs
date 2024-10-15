using AutoMapper;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models;
using Backlog_MicroService.Models.Backlog;

namespace Backlog_MicroService.Profiles
{
    public class BacklogProfile : Profile
    {
        public BacklogProfile() 
        {
            CreateMap<Backlog, BacklogDto>()
                .ReverseMap();

            CreateMap<Backlog, BacklogConfirmation>()
               .ReverseMap();
            CreateMap<BacklogConfirmation, BacklogConfirmationDto>()
                .ReverseMap();
            CreateMap<Backlog, BacklogCreationDto>()
                .ReverseMap();
            CreateMap<Backlog, BacklogUpdateDto>()
                .ReverseMap();

            CreateMap<BacklogItemDto, BacklogDto>();

        }
    }
}
