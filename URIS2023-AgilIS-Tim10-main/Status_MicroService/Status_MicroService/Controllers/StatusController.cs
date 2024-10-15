using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Status_MicroService.Data;
using Status_MicroService.Entities;
using Status_MicroService.Model;
using Status_MicroService.Services;

namespace Status_MicroService.Controllers
{

    [ApiController]
    [Route("api/status")]
    [Produces("application/json", "application/xml")]
    public class StatusController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IStatusRepository statusRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;

        public StatusController(IMapper mapper, IStatusRepository statusRepository, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.mapper = mapper;
            this.statusRepository = statusRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<Status>>> GetStatus()
        {

            List<Status> status = statusRepository.GetStatus();

            if (status == null || status.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetStatus", "Status not found.");
                NoContent();
            }

            Console.WriteLine(await loggerService.Log(LogLevel.Information, "GetStatus", "Status successfully returned."));
            return Ok(status);

        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{statusId}")]
        public async Task<ActionResult<Status>> GetStatusId(Guid statusId)
        {
            Status statusModel = statusRepository.GetStatusId(statusId);
            if (statusModel == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetStatusId", "Status with id: " + statusId + " not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetStatusId", "Status with id: " + statusId + " successfully returned.");
            return Ok(mapper.Map<StatusDto>(statusModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StatusDto>> CreateStatus([FromBody] StatusCreationDto status)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Vraćajte 400 Bad Request za validacione greške
                }


                Status mappedStatus = mapper.Map<Status>(status);
                StatusConfirmation confirmationStatus = statusRepository.AddStatus(mappedStatus);
                statusRepository.SaveChanges();

               // string location = linkGenerator.GetPathByAction("GetStatusId", "Status", new { statusId = confirmationStatus.IdStatusa });
                
                await loggerService.Log(LogLevel.Information, "CreateStatus", "Status successfully created.");
                return Ok( mapper.Map<StatusConfirmationDto>(confirmationStatus));
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "CreateStatus", "Error creating status. ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        [HttpDelete("{statusId}")]
        public async Task<IActionResult> DeleteStatus(Guid statusId)
        {
            try
            {
                Status status = statusRepository.GetStatusId(statusId);
                if (status == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteStatus", "Status with id " + statusId + " not found.");
                    return NotFound();
                }

                statusRepository.RemoveStatus(statusId);
                statusRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteStatus", "Status with id " + statusId + " succesfully deleted.");
                return StatusCode(StatusCodes.Status204NoContent, "Uspesno obrisan status!");
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "DeleteStatus", "Error deleting status with id " + statusId + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        public async Task<ActionResult<StatusDto>> UpdateStatus(StatusUpdateDto status)
        {
            try
            {
                // Map the DTO to the domain model
                Status mappedStatus = mapper.Map<Status>(status);

                // Call the repository method to update the status
                var updatedStatus = statusRepository.UpdateStatus(mappedStatus);

                // Map the updated epic to DTO
                StatusDto updateStatusDto = mapper.Map<StatusDto>(updatedStatus);

                await loggerService.Log(LogLevel.Information, "UpdateStatus", "Status with id " + status + " successfully updated.");
                // Return the updated resource
                return Ok(updatedStatus);
            }
            catch (KeyNotFoundException)
            {
                await loggerService.Log(LogLevel.Warning, "UpdateStatus", "Status with id " + status + " not found.");
                return NotFound();
            }
            catch (Exception)
            {
                await loggerService.Log(LogLevel.Error, "UpdateStatus", "Error updating status with id " + status + ".");
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


        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("backlogItem/{backlogItemId}")]
        public ActionResult<Status> GetStatusByBacklogItemId(Guid backlogItemId)
        {
            var status = statusRepository.GetStatusByBacklogItemId(backlogItemId);

            if (status == null)
            {
                loggerService.Log(LogLevel.Warning, "GetStatusByBacklogItemId", "Backlog item with id: " + backlogItemId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetStatusByBacklogItemId", "Backlog item with id: " + backlogItemId + " successfully returned.");
            Console.WriteLine(status);

            var dto = mapper.Map<StatusDto>(status);
            Console.WriteLine(dto);
            return (status); 
        }
    }
}
