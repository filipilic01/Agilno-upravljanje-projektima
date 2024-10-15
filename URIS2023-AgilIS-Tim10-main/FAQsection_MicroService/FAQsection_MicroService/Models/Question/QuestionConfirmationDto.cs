namespace FAQsection_MicroService.Models.Question
{
    public class QuestionConfirmationDto
    {

        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }

        public Guid FAQSectionId { get; set; }
    }
}
