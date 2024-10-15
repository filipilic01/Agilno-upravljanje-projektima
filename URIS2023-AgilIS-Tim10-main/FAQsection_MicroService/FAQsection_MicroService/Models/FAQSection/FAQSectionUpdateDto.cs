namespace FAQsection_MicroService.Models.FAQSection
{
    public class FAQSectionUpdateDto
    {
        public Guid FAQSectionId { get; set; }

        public int numberOfQuestions { get; set; }

        public Guid UserId { get; set; }

    }
}
