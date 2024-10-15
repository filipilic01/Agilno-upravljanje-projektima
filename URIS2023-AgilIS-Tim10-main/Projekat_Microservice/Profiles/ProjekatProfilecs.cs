using AutoMapper;
using Projekat_Microservice.Models;
using Projekat_Microservice.Models.ProjekatDto;

namespace Projekat_Microservice.Profiles
{
    public class ProjekatProfilecs: Profile
    {
        public ProjekatProfilecs()
        {
            CreateMap<Projekat, ProjekatDto>()
               .ForMember(dest => dest.ProjekatID, opt => opt.MapFrom(src => src.ProjekatID))
               .ForMember(dest => dest.NazivProjekta, opt => opt.MapFrom(src => src.NazivProjekta))
               .ForMember(dest => dest.OpisProjekta, opt => opt.MapFrom(src => src.OpisProjekta))
               .ForMember(dest => dest.DatumProjekta, opt => opt.MapFrom(src => src.DatumProjekta));
        }

    }
}
