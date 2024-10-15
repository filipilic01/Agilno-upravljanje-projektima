using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;

namespace FAQsection_MicroService.Data.Question
{
    public interface IQuestionRepository
    {
        List<FAQsection_MicroService.Entities.Question> GetQuestions();

        FAQsection_MicroService.Entities.Question GetQuestionById(Guid id);

        QuestionConfirmation CreateQuestion(FAQsection_MicroService.Entities.Question question);

        void UpdateQuestion(FAQsection_MicroService.Entities.Question question);

        void DeleteQuestion(Guid id);

        FAQsection_MicroService.Entities.Answer GetAnswerByQuestionId(Guid id);

        public bool SaveChanges();

    }
}
