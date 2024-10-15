using AutoMapper;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace FAQsection_MicroService.Data.Answer
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public AnswerRepository(Context context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public AnswerConfirmation CreateAnswer(Entities.Answer answer)
        {
            var createdAnswer = context.Add(answer);
            context.SaveChanges();
            return mapper.Map<AnswerConfirmation>(createdAnswer.Entity);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void DeleteAnswer(Guid id)
        {
            var deletedAnswer = GetAnswerById(id);
            context.Remove(deletedAnswer);
            context.SaveChanges();
        }

        public Entities.Answer GetAnswerById(Guid id)
        {
            return context.Answers.AsNoTracking().Include(b => b.question).ThenInclude(b => b.faqSection).FirstOrDefault(e => e.AnswerId == id);
        }

        public List<Entities.Answer> GetAnswers()
        {
            List<Entities.Answer> answers = context.Answers.AsNoTracking().Include(b => b.question).ThenInclude(b => b.faqSection).ToList();
            return answers;
        }

        public void UpdateAnswer(Entities.Answer answer)
        {
            var oldAnswer = context.Answers.FirstOrDefault(e => e.AnswerId == answer.AnswerId);

            if (oldAnswer != null)
            {

                oldAnswer.AnswerText = answer.AnswerText;

                context.SaveChanges();
            }
        }
    }
}
