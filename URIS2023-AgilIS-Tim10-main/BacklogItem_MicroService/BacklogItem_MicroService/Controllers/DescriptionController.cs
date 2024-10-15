using AutoMapper;
using BacklogItem_MicroService.Data.BacklogItems;
using BacklogItem_MicroService.Data.Descriptions;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs;
using BacklogItem_MicroService.Models.DTO.DescriptionDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/descriptions")]
    [Produces("application/json", "application/xml")]
    public class DescriptionController : ControllerBase
    {
        private readonly IDescriptionRepository _descriptionRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public DescriptionController(IDescriptionRepository descriptionRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _descriptionRepository = descriptionRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<Description>>> GetAllDescription()
        {

            var descriptions = await _descriptionRepository.GetAllAsync();

            if (descriptions == null || descriptions.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllDescription", "Descriptions not found.");

                return NoContent();
            }

             Console.WriteLine(await _loggerService.Log(LogLevel.Information, "GetAllDescription", "Descriptions successfully returned."));
            return Ok(_mapper.Map<List<DescriptionDto>>(descriptions));

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{descriptionId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<Description>> GetDescription(Guid descriptionId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var description = await _descriptionRepository.GetByIdAsync(descriptionId);

            if (description == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetDescription", "Description with id: " + descriptionId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetDescription", "Description with id: " + descriptionId + " successfully returned.");

            return Ok(_mapper.Map<DescriptionDto>(description));
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        [HttpDelete("{descriptionId}")]
        public async Task<IActionResult> DeleteDescription(Guid descriptionId)
        {

            try
            {
                var description = await _descriptionRepository.GetByIdAsync(descriptionId);

                if (description == null)
                {
                     await _loggerService.Log(LogLevel.Warning, "DeleteDescription", "Description with id " + descriptionId + " not found.");
                    return NotFound();
                }

                await _descriptionRepository.DeleteAsync(descriptionId);
                _loggerService.Log(LogLevel.Information, "DeleteDescription", "Description with id " + descriptionId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteDescription", "Error deleting description with id " +  descriptionId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<DescriptionConfirmationDto>> CreateDescription([FromBody] DescriptionCreationDto description)
        {
            try
            {
                Description descriptionEntity = _mapper.Map<Description>(description);
                descriptionEntity.DescriptionId = Guid.NewGuid();
                Console.WriteLine(descriptionEntity);
                DescriptionConfirmation confirmation = await _descriptionRepository.CreateDescriptionAsync(descriptionEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetDescription", "Description", new { descriptionId = confirmation.DescriptionId });



                 await _loggerService.Log(LogLevel.Information, "CreateDescription", "Description successfully created.");
                return Created(location, _mapper.Map<DescriptionConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                 await _loggerService.Log(LogLevel.Error, "CreateDescription", "Error creating description. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<DescriptionDto>> UpdateDescription([FromBody] DescriptionUpdateDto description)
        {
            try
            {
                var descriptionEntity = await _descriptionRepository.GetByIdAsync(description.DescriptionId);

                if (descriptionEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateDescription", "Acceptance criteria with id " + description.DescriptionId + " not found.");
                    return NotFound();
                }



                await _descriptionRepository.UpdateAsync(_mapper.Map<Description>(description));
                await _loggerService.Log(LogLevel.Information, "UpdateDescription", "Description with id " + description.DescriptionId + " successfully updated.");
                return Ok(description);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateDescription", "Error updating description with id " + description.DescriptionId + ".",e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }

        }
    }
}
