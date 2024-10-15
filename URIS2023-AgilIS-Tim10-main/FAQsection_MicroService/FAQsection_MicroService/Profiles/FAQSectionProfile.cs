using AutoMapper;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models.Answer;
using FAQsection_MicroService.Models;
using FAQsection_MicroService.Models.FAQSection;

namespace FAQsection_MicroService.Profiles
{
    public class FAQSectionProfile : Profile
    {
        public FAQSectionProfile()
        {
            CreateMap<FAQSection, FAQSectionDto>()
                .ForMember(dest => dest.FAQSectionId, opt => opt.MapFrom(src => src.FAQSectionId))
                .ForMember(dest => dest.numberOfQuestions, opt => opt.MapFrom(src => src.numberOfQuestions));
            CreateMap<FAQSectionCreationDto, FAQSection>();
            CreateMap<FAQSectionUpdateDto, FAQSection>()
                .ForMember(dest => dest.FAQSectionId, opt => opt.MapFrom(src => src.FAQSectionId))
                .ForMember(dest => dest.numberOfQuestions, opt => opt.MapFrom(src => src.numberOfQuestions))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<FAQSectionUpdateDto, FAQSectionDto>();
            CreateMap<FAQSection, FAQSectionConfirmation>()
                .ForMember(dest => dest.FAQSectionId, opt => opt.MapFrom(src => src.FAQSectionId))
                .ForMember(dest => dest.numberOfQuestions, opt => opt.MapFrom(src => src.numberOfQuestions))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
                
            CreateMap<FAQSectionUpdateDto, FAQSectionConfirmationDto>();
            CreateMap<FAQSectionConfirmation, FAQSectionConfirmationDto>();
        }
    }
}
