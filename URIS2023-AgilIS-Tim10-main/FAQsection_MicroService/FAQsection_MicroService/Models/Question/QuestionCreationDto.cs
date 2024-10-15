namespace FAQsection_MicroService.Models.Question
{
    public class QuestionCreationDto
    {
        public string QuestionText { get; set; }

        public Guid FAQSectionId { get; set; }
       
       
    }
}
