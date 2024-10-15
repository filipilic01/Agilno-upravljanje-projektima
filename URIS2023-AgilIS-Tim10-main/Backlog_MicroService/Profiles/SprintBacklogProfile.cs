using AutoMapper;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.Backlog;
using Backlog_MicroService.Models.SprintBacklog;

namespace Backlog_MicroService.Profiles
{
    public class SprintBacklogProfile :Profile
    {
        public SprintBacklogProfile() 
        {
            CreateMap<SprintBacklog, SprintBacklogDto>()
                .ReverseMap();
            CreateMap<SprintBacklog, SprintBacklogConfirmation>()
               .ReverseMap();
            CreateMap<SprintBacklogConfirmation, SprintBacklogConfirmationDto>()
                .ReverseMap();
            CreateMap<SprintBacklog, SprintBacklogCreationDto>()
                .ReverseMap();
            CreateMap<SprintBacklog, SprintBacklogUpdateDto>()
                .ReverseMap();

            //CreateMap<SprintBacklog, BacklogDto>();

        }
    }
}
