namespace FAQsection_MicroService.Models.Question
{
    public class QuestionUpdateDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }

        public Guid FAQSectionId { get; set; }
       
    }
}
