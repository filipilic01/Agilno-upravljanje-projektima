using AutoMapper;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;
using FAQsection_MicroService.Models.Answer;

namespace FAQsection_MicroService.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerCreationDto, Answer>();
            CreateMap<AnswerUpdateDto, Answer>();
            CreateMap<AnswerUpdateDto, AnswerDto>();
            CreateMap<Answer, AnswerConfirmation>();
            CreateMap<AnswerUpdateDto, AnswerConfirmationDto>();
            CreateMap<AnswerConfirmation, AnswerConfirmationDto>();
        }

    }
}
