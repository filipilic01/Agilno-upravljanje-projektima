using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FAQsection_MicroService.Entities
{
    public class FAQSection
    {
        [Key]
        public Guid FAQSectionId { get; set; }

        public int numberOfQuestions { get; set; }

        public ICollection<Question> Questions { get; set; }

        public Guid UserId { get; set; }

    }
}
