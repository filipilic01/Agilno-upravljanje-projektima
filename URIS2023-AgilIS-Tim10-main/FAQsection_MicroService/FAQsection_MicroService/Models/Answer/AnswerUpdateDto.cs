namespace FAQsection_MicroService.Models.Answer
{
    public class AnswerUpdateDto
    {
        public Guid AnswerId { get; set; }
        public string AnswerText { get; set; }

        public Guid QuestionId { get; set; }
       
    }
}
