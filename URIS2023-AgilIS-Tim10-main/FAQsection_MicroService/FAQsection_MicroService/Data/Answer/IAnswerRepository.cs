using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;

namespace FAQsection_MicroService.Data.Answer
{
    public interface IAnswerRepository
    {
        List<FAQsection_MicroService.Entities.Answer> GetAnswers();

        FAQsection_MicroService.Entities.Answer GetAnswerById(Guid id);

        AnswerConfirmation CreateAnswer(FAQsection_MicroService.Entities.Answer answer);

        void UpdateAnswer(FAQsection_MicroService.Entities.Answer answer);

        void DeleteAnswer(Guid id);

        public bool SaveChanges();

    }
}
