namespace FAQsection_MicroService.Models
{
    public class QuestionConfirmation
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }

        public Guid FAQSectionId { get; set; }
       
    }
}
