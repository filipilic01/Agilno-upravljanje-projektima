using AutoMapper;
using Backlog_MicroService.Data;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.Backlog;
using Backlog_MicroService.Models.SprintBacklog;
using Backlog_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backlog_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogs/sprintBacklogs")]
    [Produces("application/json", "application/xml")]
    public class SprintBacklogController : Controller
    {

        private readonly IMapper mapper;
        private readonly ISprintBacklogRepository sprintBacklogRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;


        public SprintBacklogController (IMapper mapper, ISprintBacklogRepository sprintBacklogRepository, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.mapper = mapper;
            this.sprintBacklogRepository = sprintBacklogRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<SprintBacklog>>> GetSprintBacklogs()
        {
            List<SprintBacklog> sprintBacklogs = sprintBacklogRepository.GetSprintBacklogs();
            if (sprintBacklogs == null || sprintBacklogs.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetSprintBacklogs", "Sprint backlog not found.");
                NoContent();
            }

            Console.WriteLine(await loggerService.Log(LogLevel.Information, "GetSprintBacklogs", "Sprint backlog successfully returned."));
            return Ok(mapper.Map<List<SprintBacklogDto>>(sprintBacklogs));
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{sprintBacklogId}")]
        public async Task<ActionResult<SprintBacklog>> GetSprintBacklog(Guid sprintBacklogId)
        {
            SprintBacklog sprintBacklogModel = sprintBacklogRepository.GetSprintBacklogId(sprintBacklogId);
            if (sprintBacklogModel == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetSprintBacklog", "Sprint backlog with id: " + sprintBacklogId + " not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetSprintBacklog", "Sprint backlog with id: " + sprintBacklogId + " successfully returned.");
            return Ok(mapper.Map<SprintBacklogDto>(sprintBacklogModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        public async Task<ActionResult<SprintBacklogConfirmation>> CreateSprintBacklog([FromBody] SprintBacklogCreationDto sprintBacklog)
        {
            try
            {
                SprintBacklog mappedSprintBacklog = mapper.Map<SprintBacklog>(sprintBacklog);
                mappedSprintBacklog.IdBacklog = Guid.NewGuid();
                SprintBacklogConfirmation confirmationSprintBacklog = sprintBacklogRepository.AddSprintBacklog(mappedSprintBacklog);
                bool modelValid = ValidateSprintDate(mappedSprintBacklog);
                sprintBacklogRepository.SaveChanges();

                //string location = linkGenerator.GetPathByAction("GetSprintBacklog", "SprintBacklog", new { idBacklog = confirmationSprintBacklog.IdBacklog });

                if (!modelValid)
                {
                    sprintBacklogRepository.RemoveSprintBacklog(mappedSprintBacklog.IdBacklog);
                    return BadRequest("Sprint dates do not match. Beginning date must be earlier than ending date.");
                }

                await loggerService.Log(LogLevel.Information, "CreateSprintBacklog", "Sprint backlog successfully created.");
                return Ok( mapper.Map<SprintBacklogConfirmation>(confirmationSprintBacklog));
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "CreateSprintBacklog", "Error creating sprint backlog. ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }



        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        [HttpDelete("{sprintBacklogId}")]
        public async Task<IActionResult> DeleteSprintBacklog(Guid sprintBacklogId)
        {
            try
            {
                SprintBacklog sprintBacklog = sprintBacklogRepository.GetSprintBacklogId(sprintBacklogId);
                if (sprintBacklog == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteSprintBacklog", "Sprint backlog with id " + sprintBacklogId + " not found.");
                    return NotFound();
                }

                sprintBacklogRepository.RemoveSprintBacklog(sprintBacklogId);
                sprintBacklogRepository.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, "Uspesno obrisan sprint backlog!");
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "DeleteSprintBacklog", "Error deleting sprint backlog with id " + sprintBacklogId + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SprintBacklogDto>> UpdateSprintBacklog(SprintBacklogUpdateDto sprintBacklog)
        {
            try
            {
                // Map the DTO to the domain model
                SprintBacklog mappedSprintBacklog = mapper.Map<SprintBacklog>(sprintBacklog);

                // Call the repository method to update the backlog
                var updatedSprintBacklog = sprintBacklogRepository.UpdateSprintBacklog(mappedSprintBacklog);

                // Map the updated epic to DTO
                SprintBacklogDto updatedSprintBacklogDto = mapper.Map<SprintBacklogDto>(updatedSprintBacklog);

                await loggerService.Log(LogLevel.Information, "UpdateSprintBacklog", "Sprint backlog with id " + sprintBacklog + " successfully updated.");
                // Return the updated resource
                return Ok(updatedSprintBacklog);
            }
            catch (KeyNotFoundException)
            {
                await loggerService.Log(LogLevel.Warning, "UpdateSprintBacklog", "Sprint backlog with id " + sprintBacklog + " not found.");
                return NotFound();
            }
            catch (Exception)
            {
                await loggerService.Log(LogLevel.Error, "UpdateSprintBacklog", "Error updating sprint backlog with id " + sprintBacklog + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        private bool ValidateSprintDate(SprintBacklog sprint)
        {
            if (sprint.Pocetak >= sprint.Kraj)
            {
                return false;
            }
            return true;
        }

        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }
    }
}
