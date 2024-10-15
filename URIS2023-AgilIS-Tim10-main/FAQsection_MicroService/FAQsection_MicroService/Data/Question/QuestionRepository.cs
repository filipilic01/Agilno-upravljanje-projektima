using AutoMapper;
using FAQsection_MicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace FAQsection_MicroService.Data.Question
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public QuestionRepository(Context context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public QuestionConfirmation CreateQuestion(Entities.Question question)
        {
            var createdQuestion = context.Add(question);
            context.SaveChanges();
            return mapper.Map<QuestionConfirmation>(createdQuestion.Entity);
        }

        public void DeleteQuestion(Guid id)
        {
            var deletedQuestion = GetQuestionById(id);
            context.Remove(deletedQuestion);
            context.SaveChanges();
        }

        public Entities.Question GetQuestionById(Guid id)
        {
            return context.Questions.AsNoTracking().Include(b => b.faqSection).FirstOrDefault(e => e.QuestionId == id);
        }

        public List<Entities.Question> GetQuestions()
        {
            return context.Questions.AsNoTracking().Include(b => b.faqSection).ToList();
        }

        public void UpdateQuestion(Entities.Question question)
        {
            var oldQuestion = context.Questions.FirstOrDefault(e => e.QuestionId == question.QuestionId);

            if (oldQuestion != null)
            {

                oldQuestion.QuestionText = question.QuestionText;

                context.SaveChanges();
            }
        }

        public Entities.Answer GetAnswerByQuestionId(Guid id)
        {
            return context.Answers.AsNoTracking().Include(b => b.question).ThenInclude(b => b.faqSection).FirstOrDefault(e => e.QuestionId == id);
        }
    }
}
