using AutoMapper;
using Rag_MicroService.Models;
using Rag_MicroService.Models.DTO;
using Rag_MicroService.Models.Entities;

namespace Rag_MicroService.Profiles
{
    public class RagProfile : Profile
    {
        public RagProfile() 
        { 
            CreateMap<Rag, RagDto>();
            CreateMap<RagCreationDto, Rag>();
            CreateMap<RagUpdateDto, Rag>();
            CreateMap<RagUpdateDto, RagDto>();
            CreateMap<Rag, RagConfirmation>();
            CreateMap<RagUpdateDto, RagConfirmationDto>();
            CreateMap<RagConfirmation, RagConfirmationDto>();
        }
    }
}
