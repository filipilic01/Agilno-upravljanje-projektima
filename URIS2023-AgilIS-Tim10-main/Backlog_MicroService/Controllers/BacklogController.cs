using AutoMapper;
using Backlog_MicroService.Data;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models;
using Backlog_MicroService.Models.Backlog;
using Backlog_MicroService.Models.ProductBacklog;
using Backlog_MicroService.Models.SprintBacklog;
using Backlog_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace Backlog_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogs")]
    [Produces("application/json", "application/xml")]

    public class BacklogController : Controller
    {
        private readonly IMapper mapper;
        private readonly IBacklogRepository backlogRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;
        private readonly IConfiguration configuration;
        private readonly IServiceCall backlogItemService;


        public BacklogController(IMapper mapper, IBacklogRepository backlogRepository, LinkGenerator linkGenerator, ILoggerService loggerService, IConfiguration configuration, IServiceCall backlogItemService)
        {
            this.mapper = mapper;
            this.backlogRepository = backlogRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
            this.configuration = configuration;
            this.backlogItemService = backlogItemService;
        }


        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<Backlog>>> GetBacklogs()
        {

            List<Backlog> backlogs = backlogRepository.GetBacklogs();
            

            if (backlogs == null || backlogs.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogs", "Backlogs not found");

                return NoContent();
            }

            Console.WriteLine(await loggerService.Log(LogLevel.Information, "GetBacklogs", "Backlogs successfully returned."));
            return Ok(backlogs);

        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{backlogId}")]
        public async Task<ActionResult<Backlog>> GetBacklog(Guid backlogId)
        {
            Backlog backlogModel = backlogRepository.GetBacklogId(backlogId);
            if (backlogModel == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklog", "Backlog with id " + backlogId + " not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetBacklog", "Backlog item with id: " + backlogId + " successfully returned.");
            return Ok(mapper.Map<BacklogDto>(backlogModel));
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult<BacklogDto>> CreateBacklog([FromBody] BacklogDto backlog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Vraćajte 400 Bad Request za validacione greške
                }


                Backlog mappedBacklog = mapper.Map<Backlog>(backlog);
                BacklogConfirmation confirmationBacklog = backlogRepository.AddBacklog(mappedBacklog);
                backlogRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetBacklog", "Backlog", new { backlogId = confirmationBacklog.IdBacklog });
                await loggerService.Log(LogLevel.Information, "CreateBacklog", "Backlog successfully created.");
                return Created(location, mapper.Map<BacklogConfirmationDto>(confirmationBacklog));
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "CreateBacklogItem", "Error creating backlog item. ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        [HttpDelete("{backlogId}")]
        public async Task<IActionResult> DeleteBacklog(Guid backlogId)
        {
            try
            {
                Backlog backlog = backlogRepository.GetBacklogId(backlogId);
                if (backlog == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteBacklog", "Backlog with id " + backlogId + " not found.");
                    return NotFound();
                }

                backlogRepository.RemoveBacklog(backlogId);
                backlogRepository.SaveChanges();
               
                return StatusCode(StatusCodes.Status204NoContent, "Uspesno obrisan backlog!");
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "DeleteBacklog", "Error deleting backlog with id " + backlogId + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<BacklogDto>> UpdateBacklog(BacklogUpdateDto backlog)
        {
            try
            {
                // Map the DTO to the domain model
                Backlog mappedBacklog = mapper.Map<Backlog>(backlog);

                // Call the repository method to update the backlog
                var updatedBacklog = backlogRepository.UpdateBacklog(mappedBacklog);

                // Map the updated epic to DTO
                BacklogDto updatedBacklogDto = mapper.Map<BacklogDto>(updatedBacklog);

                // Return the updated resource
                return Ok(updatedBacklogDto);
            }
            catch (KeyNotFoundException)
            {
                await loggerService.Log(LogLevel.Warning, "UpdateBacklog", "Backlog with id " + backlog + " not found.");
                return NotFound();
            }
            catch (Exception)
            {
                await loggerService.Log(LogLevel.Error, "UpdateBacklog", "Error updating backlog with id " + backlog + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("productBacklogs/user/{korisnikId}")]
        public ActionResult<ProductBacklogDto> GetProductBacklogByKorisnikId(Guid korisnikId)
        {
            var procuctBacklog = backlogRepository.GetProductBacklogByKorisnikId(korisnikId);

            if (procuctBacklog == null)
            {
                loggerService.Log(LogLevel.Warning, "GetBacklog", "Korisnik with id: " + korisnikId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetBacklog", "Korisnik with id: " + korisnikId + " successfully returned.");
            return Ok(mapper.Map<ProductBacklogDto>(procuctBacklog));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("sprintBacklogs/user/{korisnikId}")]
        public ActionResult<SprintBacklogDto> GetSprintBacklogByKorisnikId(Guid korisnikId)
        {
            var sprintBacklog = backlogRepository.GetSprintBacklogByKorisnikId(korisnikId);

            if (sprintBacklog == null)
            {
                loggerService.Log(LogLevel.Warning, "GetBacklog", "Korisnik with id: " + korisnikId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetBacklog", "Korisnik with id: " + korisnikId + " successfully returned.");
            return Ok(mapper.Map<SprintBacklogDto>(sprintBacklog));
        }

        [HttpGet("backlogItem/{backlogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogItemDto>>> GetBacklogItemsForBacklog(Guid backlogId)
        {
            string url = configuration["Services:BacklogItemService"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var backlogItemDto = await backlogItemService.SendGetRequestAsync(url + "/" + backlogId, token);
            Console.WriteLine(backlogItemDto);

            if (backlogItemDto is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetBacklogItemsForBacklog", "Backlog item list for backlog with id: " + backlogId + " successfully returned.");
                
                return Ok(backlogItemDto);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogItemsForBacklog", "Backlog item list for backlog  with id: " + backlogId + " not found");
                return NoContent();
            }

        }

        [HttpGet("epics/{backlogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogItemDto>>> GetEpicsForBacklog(Guid backlogId)
        {
            string url = configuration["Services:BacklogItemService-Epic"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var backlogItemDto = await backlogItemService.SendGetRequestAsync(url + "/" + backlogId, token);
            Console.WriteLine(backlogItemDto);

            if (backlogItemDto is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetEpicsForBacklog", "Epic list for backlog with id: " + backlogId + " successfully returned.");

                return Ok(backlogItemDto);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetEpicsForBacklog", "Epic list for backlog item with id: " + backlogId + " not found");
                return NoContent();
            }

        }

        [HttpGet("userStories/{backlogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogItemDto>>> GetUserStoriesForBacklog(Guid backlogId)
        {
            string url = configuration["Services:BacklogItemService-UserStory"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var backlogItemDto = await backlogItemService.SendGetRequestAsync(url + "/" + backlogId, token);
            Console.WriteLine(backlogItemDto);

            if (backlogItemDto is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetUserStoriesForBacklog", "User stories for backlog with id: " + backlogId + " successfully returned.");

                return Ok(backlogItemDto);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetUserStoriesForBacklog", "User stories for backlog item with id: " + backlogId + " not found");
                return NoContent();
            }

        }
        [HttpGet("functionalities/{backlogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogItemDto>>> GetFunctionalitiesForBacklog(Guid backlogId)
        {
            string url = configuration["Services:BacklogItemService-Functionality"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var backlogItemDto = await backlogItemService.SendGetRequestAsync(url + "/" + backlogId, token);
            Console.WriteLine(backlogItemDto);

            if (backlogItemDto is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetFunctionalitiesForBacklog", "Functionalities for backlog with id: " + backlogId + " successfully returned.");

                return Ok(backlogItemDto);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetFunctionalitiesForBacklog", "Functionalities for backlog item with id: " + backlogId + " not found");
                return NoContent();
            }

        }

        [HttpGet("tasks/{backlogId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogItemDto>>> GetTasksForBacklog(Guid backlogId)
        {
            string url = configuration["Services:BacklogItemService-Task"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var backlogItemDto = await backlogItemService.SendGetRequestAsync(url + "/" + backlogId, token);
            Console.WriteLine(backlogItemDto);

            if (backlogItemDto is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetFunctionalitiesForBacklog", "Tasks for backlog with id: " + backlogId + " successfully returned.");

                return Ok(backlogItemDto);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetFunctionalitiesForBacklog", "Tasks for backlog item with id: " + backlogId + " not found");
                return NoContent();
            }

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("sprint/projekat/{projekatId}")]
        public ActionResult<SprintBacklog> GetSprintBacklogByProjekatId(Guid projekatId)
        {
           SprintBacklog Backlog = backlogRepository.GetSprintBacklogByProjekatId(projekatId);

            if (Backlog == null)
            {
                loggerService.Log(LogLevel.Warning, "GetSprintBacklogByProjekatId", "Projekat with id: " + projekatId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetSprintBacklogByProjekatId", "Projekat with id: " + projekatId + " successfully returned.");
            return Ok(Backlog);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("product/projekat/{projekatId}")]
        public ActionResult<ProductBacklog> GetProductBacklogByProjekatId(Guid projekatId)
        {
            ProductBacklog Backlog = backlogRepository.GetProductBacklogByProjekatId(projekatId);

            if (Backlog == null)
            {
                loggerService.Log(LogLevel.Warning, "GetProductBacklogByProjekatId", "Projekat with id: " + projekatId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetProductBacklogByProjekatId", "Projekat with id: " + projekatId + " successfully returned.");
            return Ok(Backlog);
        }
    }
}
