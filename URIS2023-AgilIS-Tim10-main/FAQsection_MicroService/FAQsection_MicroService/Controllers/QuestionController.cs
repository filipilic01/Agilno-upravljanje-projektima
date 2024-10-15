using AutoMapper;
using FAQsection_MicroService.Data.Question; 
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;
using FAQsection_MicroService.Models.Question;
using FAQsection_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAQsection_MicroService.Controllers
{
    [ApiController]
    [Route("api/FAQSection/question")]
    [Produces("application/json", "application/xml")]
    public class QuestionController : Controller

    {
        private readonly IMapper mapper;
        private readonly IQuestionRepository questionRepository; 
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;


        public QuestionController(IMapper mapper, IQuestionRepository questionRepository, LinkGenerator linkGenerator, ILoggerService loggerService) 
        {
            this.mapper = mapper;
            this.questionRepository = questionRepository; 
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;

        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<List<Question>> GetQuestions() 
        {
            List<Question> question = questionRepository.GetQuestions(); 

            if (question == null || question.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetQuestions", "Questions not found.");
                NoContent();
            }
            Console.WriteLine(loggerService.Log(LogLevel.Information, "GetQuestions", "Questions  successfully returned."));
            return Ok(question);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{questionId}")]
        public ActionResult<Question> GetQuestionById(Guid questionId) 
        {
            Question question = questionRepository.GetQuestionById(questionId); 
            if (question == null)
            {
                loggerService.Log(LogLevel.Warning, "GetQuestionById", "Question with id: " + questionId + " not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetQuestionById", "Question with id: " + questionId + " successfully returned.");
            return Ok(mapper.Map<Question>(question));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<QuestionDto> CreateQuestion([FromBody] QuestionCreationDto question) 
        {
            try
            {
                Question questionEntity = mapper.Map<Question>(question); 
                QuestionConfirmation confirmation = questionRepository.CreateQuestion(questionEntity); 

               // string location = linkGenerator.GetPathByAction("GetQuestion", "Question", new { QuestionId = confirmation.QuestionId });
                loggerService.Log(LogLevel.Information, "CreateQuestion", "Question successfully created.");
                return Ok( mapper.Map<QuestionConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreateQuestion", "Error creating question. ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{questionId}")]
        public IActionResult DeleteQuestion(Guid questionId) 
        {
            try
            {
                Question question = questionRepository.GetQuestionById(questionId); 
                if (question == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteQuestion", "Question with id " + questionId + " not found.");
                    return NotFound();
                }

                questionRepository.DeleteQuestion(questionId); 
                questionRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteQuestion", "Question with id " + questionId + " succesfully deleted.");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                loggerService.Log(LogLevel.Error, "DeleteQuestion", "Error deleting question. ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<QuestionDto> UpdateQuestion(QuestionUpdateDto question) 
        {
            try
            {
                var questionEntity = questionRepository.GetQuestionById(question.QuestionId); 

                if (questionEntity == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateQuestion", "Question with id " + question.QuestionId + " not found.");
                    return NotFound();
                }

                questionRepository.UpdateQuestion(mapper.Map<Question>(question));
                loggerService.Log(LogLevel.Information, "UpdateQuestion", "Question with id " + question.QuestionId + " successfully updated.");
                return Ok(question);
            }
            catch (Exception e)
            {
                loggerService.Log(LogLevel.Error, "UpdateQuestion", "Error updating question with id " + question.QuestionId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("answer/{questionId}")] 
        public ActionResult<Answer> GetAnswerByQuestionId(Guid questionId) 
        {
            var answer =  questionRepository.GetAnswerByQuestionId(questionId);

            if (answer == null)
            {
                 loggerService.Log(LogLevel.Warning, "GetAnswerByQuestionId", "Question with id: " + questionId + " not found.");
                return NotFound();
            }

             loggerService.Log(LogLevel.Information, "GetAnswerByQuestionId", "Question  with id: " + questionId + " successfully returned.");

            return Ok(answer);
        }
    }
}
