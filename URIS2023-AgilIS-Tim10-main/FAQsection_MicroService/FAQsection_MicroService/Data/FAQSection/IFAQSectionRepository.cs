using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;

namespace FAQsection_MicroService.Data.FAQSection
{
    public interface IFAQSectionRepository
    {
        List<FAQsection_MicroService.Entities.FAQSection> GetFAQSections();

        FAQsection_MicroService.Entities.FAQSection GetFAQSectionById(Guid id);

        FAQSectionConfirmation CreateFAQSection(FAQsection_MicroService.Entities.FAQSection faqSection);

        void UpdateFAQSection(FAQsection_MicroService.Entities.FAQSection faqSection);

        void DeleteFAQSection(Guid id);

        public bool SaveChanges();

        FAQsection_MicroService.Entities.FAQSection GetFAQSectionByUserId(Guid id);

        List<FAQsection_MicroService.Entities.Question> GetQuestionsByFAQId (Guid id);

    }
}
