using AutoMapper;
using FAQsection_MicroService.Data.Answer;
using FAQsection_MicroService.Data.FAQSection;
using FAQsection_MicroService.Entities;
using FAQsection_MicroService.Models.Answer;
using FAQsection_MicroService.Models;
using Microsoft.AspNetCore.Mvc;
using FAQsection_MicroService.Models.FAQSection;
using FAQsection_MicroService.Services;
using Microsoft.AspNetCore.Authorization;

namespace FAQsection_MicroService.Controllers
{
    [ApiController]
    [Route("api/FAQSection")]
    [Produces("application/json", "application/xml")]
    public class FAQSectionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFAQSectionRepository faqSectionRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;


        public FAQSectionController(IMapper mapper, IFAQSectionRepository faqSectionRepository, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.mapper = mapper;
            this.faqSectionRepository = faqSectionRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;

        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<List<FAQSection>> GetFAQSections()
        {

            List<FAQSection> fAQSection = faqSectionRepository.GetFAQSections();

            if (fAQSection == null || fAQSection.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetFAQSections", "FAQ section not found.");

                NoContent();
            }
            Console.WriteLine(loggerService.Log(LogLevel.Information, "GetFAQSections", "FAQ section successfully returned."));

            return Ok(fAQSection);

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{faqSectionId}")]
        public ActionResult<FAQSection> GetFAQSectionById(Guid faqSectionId)
        {
            FAQSection faqSection = faqSectionRepository.GetFAQSectionById(faqSectionId);
            if (faqSection == null)
            {
                loggerService.Log(LogLevel.Warning, "GetFAQSectionById", "FAQ section with id: " + faqSectionId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetFAQSectionById", "FAQ section with id: " + faqSectionId + " successfully returned.");

            return Ok(mapper.Map<FAQSection>(faqSection));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<FAQSectionDto> CreateFAQSection([FromBody] FAQSectionCreationDto faqSection)
        {
            try
            {
                FAQSection faqSectionEntity = mapper.Map<FAQSection>(faqSection);
                faqSectionEntity.FAQSectionId = Guid.NewGuid();
                FAQSectionConfirmation confirmation = faqSectionRepository.CreateFAQSection(faqSectionEntity);

                string location = linkGenerator.GetPathByAction("GetFAQSection", "FAQSection", new { FAQSectionId = confirmation.FAQSectionId });
                loggerService.Log(LogLevel.Information, "CreateFAQSection", "FAQ section successfully created.");

                return Ok( mapper.Map<FAQSectionConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreateFAQSection", "Error creating FAQ section. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{faqSectionId}")]
        public IActionResult DeleteFAQSection(Guid faqSectionId)
        {
            try
            {
                FAQSection faqSection = faqSectionRepository.GetFAQSectionById(faqSectionId);
                if (faqSection == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteFAQSection", "FAQ section with id " + faqSectionId + " not found.");
                    return NotFound();
                }

                faqSectionRepository.DeleteFAQSection(faqSectionId);
                faqSectionRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteFAQSection", "FAQ section with id " + faqSectionId + " succesfully deleted.");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                loggerService.Log(LogLevel.Error, "DeleteFAQSection", "Error deleting FAQ section. ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<FAQSectionDto> UpdateFAQSection(FAQSectionUpdateDto faqSection)
        {
            try
            {
                var faqSectionEntity = faqSectionRepository.GetFAQSectionById(faqSection.FAQSectionId);

                if (faqSectionEntity == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateFAQSection", "FAQ section with id " + faqSection.FAQSectionId + " not found.");
                    return NotFound();
                }

                faqSectionRepository.UpdateFAQSection(mapper.Map<FAQSection>(faqSection));
                loggerService.Log(LogLevel.Information, "UpdateFAQSection", "FAQ section with id " + faqSection.FAQSectionId + " successfully updated.");
                return Ok(faqSection);
            }
            catch (Exception e)
            {
                loggerService.Log(LogLevel.Error, "UpdateFAQSection", "Error updating FAQ section with id " + faqSection.FAQSectionId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("questions/{faqId}")]
        public ActionResult<List<Question>> GetQuestionsByFAQId(Guid faqId)
        {
            var questions = faqSectionRepository.GetQuestionsByFAQId(faqId);

            if (questions == null)
            {
                loggerService.Log(LogLevel.Warning, "GetQuestionsByFAQId", "FAQ section with id: " + faqId + " not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetQuestionsByFAQId", "FAQ section with id: " + faqId + " successfully returned.");
            return Ok(questions);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("user/{userId}")]
        public ActionResult<FAQSectionDto> GetFAQSectionByUserId(Guid userId)
        {
            var faqSection = faqSectionRepository.GetFAQSectionByUserId(userId);

            if (faqSection == null)
            {
                loggerService.Log(LogLevel.Warning, "GetFAQSectionById", "User item with id: " + userId + " not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetFAQSectionById", "User with id: " + userId + " successfully returned.");
            return Ok(mapper.Map<FAQSectionDto>(faqSection));
        }


    }
}
