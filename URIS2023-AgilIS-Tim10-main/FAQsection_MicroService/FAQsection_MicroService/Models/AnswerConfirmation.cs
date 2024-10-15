namespace FAQsection_MicroService.Models
{
    public class AnswerConfirmation
    {
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Guid QuestionId { get; set; }
       
    }
}
