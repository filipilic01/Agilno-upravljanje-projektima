namespace FAQsection_MicroService.Models.Answer
{
    public class AnswerConfirmationDto
    {
        public Guid AnswerId {get; set;}
        public string AnswerText { get; set; }

        public Guid QuestionId { get; set; }
 
    }
}
