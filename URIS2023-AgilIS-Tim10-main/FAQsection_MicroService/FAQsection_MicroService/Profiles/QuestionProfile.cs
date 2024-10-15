using AutoMapper;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models.FAQSection;
using FAQsection_MicroService.Models;
using FAQsection_MicroService.Models.Question;

namespace FAQsection_MicroService.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionCreationDto, Question>();
            CreateMap<QuestionUpdateDto, Question>();
            CreateMap<QuestionUpdateDto, QuestionDto>();
            CreateMap<Question, QuestionConfirmation>();
            CreateMap<QuestionUpdateDto, QuestionConfirmationDto>();
            CreateMap<QuestionConfirmation, QuestionConfirmationDto>();
        }
    }
}
