using AutoMapper;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.ProductBacklog;

namespace Backlog_MicroService.Profiles
{
    public class ProductBacklogProfile :Profile
    {
        public ProductBacklogProfile()
        {
            CreateMap<ProductBacklog, ProductBacklogDto>()
                .ReverseMap();

            CreateMap<ProductBacklog, ProductBacklogConfirmation>()
                   .ReverseMap();

            CreateMap<ProductBacklogConfirmation, ProductBacklogConfirmationDto>()
                    .ReverseMap();

            CreateMap<ProductBacklog, ProductBacklogCreationDto>()
                    .ReverseMap();

            CreateMap<ProductBacklog, ProductBacklogUpdateDto>()
                    .ReverseMap();

        }
    }
}
