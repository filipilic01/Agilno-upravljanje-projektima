using AutoMapper;
using FAQsection_MicroService.Data.Answer;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models;
using FAQsection_MicroService.Models.Answer;
using FAQsection_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FAQsection_MicroService.Controllers
{
    [ApiController]
    [Route("api/FAQSection/answer")]
    [Produces("application/json", "application/xml")]
    public class AnswerController : Controller

    {
        private readonly IMapper mapper;
        private readonly IAnswerRepository answerRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;


        public AnswerController(IMapper mapper, IAnswerRepository answerRepository, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.mapper = mapper;
            this.answerRepository = answerRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;

        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<List<Answer>> GetAnswers()
        {

            List<Answer> answer = answerRepository.GetAnswers();

            if (answer == null || answer.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetAnswers", "Aswers not found.");
                NoContent();
            }
            Console.WriteLine(loggerService.Log(LogLevel.Information, "GetAnswers", "Answers successfully returned."));
            return Ok(answer);

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{answerId}")]
        public ActionResult<Answer> GetAnswerById(Guid answerId)
        {
            Answer answer = answerRepository.GetAnswerById(answerId);
            if (answer == null)
            {
                loggerService.Log(LogLevel.Warning, "GetAnswerById", "Answer with id: " + answerId + " not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetAnswerById", "Answer with id: " + answerId + " successfully returned.");
            return Ok(mapper.Map<Answer>(answer));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<AnswerDto> CreateAnswer([FromBody] AnswerCreationDto answer)
        {
            try
            {
                Answer answerEntity = mapper.Map<Answer>(answer);
                answerEntity.AnswerId = Guid.NewGuid();
                AnswerConfirmation confirmation = answerRepository.CreateAnswer(answerEntity);
                //string location = linkGenerator.GetPathByAction("GetAnswer", "Answer", new { AnswerId = confirmation.AnswerId });
                loggerService.Log(LogLevel.Information, "CreateAnswer", "Answer successfully created.");
                return Ok( mapper.Map<AnswerConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreateAnswer", "Error creating answer. ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{answerId}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public IActionResult DeleteAnswer(Guid answerId)
        {
            try
            {
                Answer answer = answerRepository.GetAnswerById(answerId);
                if (answer == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteAnswer", "Answer with id " + answerId + " not found.");
                    return NotFound();
                }

                answerRepository.DeleteAnswer(answerId);
                answerRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteAnswer", "Answer with id " + answerId + " succesfully deleted.");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                loggerService.Log(LogLevel.Error, "DeleteAnswer", "Error deleting answer with id " + answerId + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<AnswerDto> UpdateAnswer(AnswerUpdateDto answer)
        {
            try
            {
                var answerEntity = answerRepository.GetAnswerById(answer.AnswerId);

                if (answerEntity == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateAnswer", "Answer with id " + answer.AnswerId + " not found.");
                    return NotFound();
                }

                answerRepository.UpdateAnswer(mapper.Map<Answer>(answer));
                loggerService.Log(LogLevel.Information, "UpdateAnswer", "Answer with id " + answer.AnswerId + " successfully updated.");
                return Ok(mapper.Map<AnswerConfirmationDto>(answer));
            }
            catch (Exception e)
            {
                loggerService.Log(LogLevel.Error, "UpdateAnswer", "Error updating answer with id " + answer.AnswerId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }
        }
    }
}
