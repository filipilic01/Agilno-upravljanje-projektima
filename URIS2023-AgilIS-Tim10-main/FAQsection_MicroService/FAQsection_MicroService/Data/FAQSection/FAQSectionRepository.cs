using AutoMapper;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;

namespace FAQsection_MicroService.Data.FAQSection
{
    public class FAQSectionRepository : IFAQSectionRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public FAQSectionRepository(Context context, IMapper mapper)
        {

            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public FAQSectionConfirmation CreateFAQSection(Entities.FAQSection faqSection)
        {
            var createdFAQSection = context.Add(faqSection);
            context.SaveChanges();
            return mapper.Map<FAQSectionConfirmation>(createdFAQSection.Entity);
        }

        public void DeleteFAQSection(Guid id)
        {
            var deletedFAQSection = GetFAQSectionById(id);
            context.Remove(deletedFAQSection);
            context.SaveChanges();
        }

        public Entities.FAQSection GetFAQSectionById(Guid id)
        {
            return context.FAQSections.FirstOrDefault(e => e.FAQSectionId == id);
        }

        public List<Entities.FAQSection> GetFAQSections()
        {
            return context.FAQSections.ToList();
        }

        public void UpdateFAQSection(Entities.FAQSection faqSection)
        {
            var oldFAQSection = context.FAQSections.FirstOrDefault(e => e.FAQSectionId == faqSection.FAQSectionId);

            if (oldFAQSection != null)
            {

                oldFAQSection.numberOfQuestions = faqSection.numberOfQuestions;

                context.SaveChanges();
            }
        }

        public Entities.FAQSection GetFAQSectionByUserId(Guid id)
        {
            return context.FAQSections.FirstOrDefault(e => e.UserId == id);
        }

        public List<Entities.Question> GetQuestionsByFAQId(Guid id)
        {
            var question = context.Questions.Where(e => e.FAQSectionId == id).ToList();
            return question;
        }
    }
}
