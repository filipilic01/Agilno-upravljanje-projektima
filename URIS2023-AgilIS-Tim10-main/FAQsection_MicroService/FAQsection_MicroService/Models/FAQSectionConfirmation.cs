namespace FAQsection_MicroService.Models
{
    public class FAQSectionConfirmation
    {
        public Guid FAQSectionId { get; set; }

        public int numberOfQuestions { get; set; }
        
        public Guid UserId { get; set; }
    }
}
