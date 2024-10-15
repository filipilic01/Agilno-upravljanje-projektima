using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FAQsection_MicroService.Entities
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }

        public string QuestionText { get; set; }

        public Guid FAQSectionId { get; set; }
        
        public FAQSection faqSection { get; set; }
        
        public Answer answer { get; set; }

    }
}
