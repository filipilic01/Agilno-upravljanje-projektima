using AutoMapper;
using Tim_Microservice.Models;
using Tim_Microservice.VO;

namespace Tim_Microservice.Profiles
{
    public class ClanTimaProfile: Profile
    {
       public ClanTimaProfile()
       {
            CreateMap<ClanTimaCreationDto, ClanTima>()
            .ForMember(dest => dest.ClanTimaId, opt => opt.MapFrom(src => src.ClanTimaId))
            .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.userName))
            .ForMember(dest => dest.TimId, opt => opt.MapFrom(src => src.TimId));

        }
    }
}
