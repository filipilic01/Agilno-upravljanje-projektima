namespace FAQsection_MicroService.Models.Answer
{
    public class AnswerCreationDto
    {

        public string AnswerText { get; set; }

        public Guid QuestionId { get; set; }
        
    }
}
