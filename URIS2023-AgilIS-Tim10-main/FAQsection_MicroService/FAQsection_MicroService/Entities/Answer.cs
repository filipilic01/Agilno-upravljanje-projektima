using System.ComponentModel.DataAnnotations;

namespace FAQsection_MicroService.Entities
{
    public class Answer
    {
        [Key]
        public Guid AnswerId { get; set; }

        public string AnswerText { get; set; }

        public Guid QuestionId { get; set; }
        public Question question { get; set; }

    }
}
