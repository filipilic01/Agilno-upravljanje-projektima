using AutoMapper;
using BacklogItem_MicroService.Data.Epics;
using BacklogItem_MicroService.Data.Functionalities;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.EpicDTOs;
using BacklogItem_MicroService.Models.DTO.FunctionalityDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/functionalities")]
    [Produces("application/json", "application/xml")]
    public class FunctionalityController : ControllerBase
    {
        private readonly IFunctionalityRepository _functionalityRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public FunctionalityController(IFunctionalityRepository functionalityRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _functionalityRepository = functionalityRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }


        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<Functionality>>> GetAllFunctionalities()
        {
            var items = await _functionalityRepository.GetAllAsync();

            if (items == null || items.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllFunctionalities", "Functionality backlog items not found.");

                return NoContent();
            }

            await _loggerService.Log(LogLevel.Information, "GetAllFunctionalities", "Functionality backlog items successfully returned.");
            return Ok(_mapper.Map<List<FunctionalityDto>>(items));

        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
     
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<Functionality>> GetFunctionality(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var item = await _functionalityRepository.GetByIdAsync(backlogItemId);

            if (item == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetFunctionality", "Functionality backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetFunctionality", "Functionality backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<FunctionalityDto>(item));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{backlogItemId}")]
        public async Task<IActionResult> DeleteFunctionality(Guid backlogItemId)
        {

            try
            {
                var item = await _functionalityRepository.GetByIdAsync(backlogItemId);

                if (item == null)
                {
                     await _loggerService.Log(LogLevel.Warning, "DeleteFunctionality", "Functionality backlog item with id " + backlogItemId + " not found.");
                    return NotFound();
                }

                await _functionalityRepository.DeleteAsync(backlogItemId);
                await _loggerService.Log(LogLevel.Information, "DeleteFunctionality", "Functionality backlog item with id " + backlogItemId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteFunctionality", "Error deleting functionality backlog item with id " +  backlogItemId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<FunctionalityConfirmationDto>> CreateFunctionality([FromBody] FunctionalityCreationDto functionality)
        {
            try
            {
                Functionality functionalityEntity = _mapper.Map<Functionality>(functionality);
                functionalityEntity.BacklogItemId = Guid.NewGuid();
                Console.WriteLine(functionalityEntity);
                FunctionalityConfirmation confirmation = await _functionalityRepository.CreateFunctionalityAsync(functionalityEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetFunctionality", "Functionality", new { backlogItemId = confirmation.BacklogItemId });



                 await _loggerService.Log(LogLevel.Information, "CreateFunctionality", "Functionality backlog item successfully created.");
                return Created(location, _mapper.Map<FunctionalityConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                 await _loggerService.Log(LogLevel.Error, "CreateFunctionality", "Error creating functionality backlog item. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<FunctionalityDto>> UpdateFunctionality([FromBody] FunctionalityUpdateDto functionality)
        {
            try
            {
                var functionalityEntity = await _functionalityRepository.GetByIdAsync(functionality.BacklogItemId);

                if (functionalityEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateFunctionality", "Functionality backlog item with id " + functionality.BacklogItemId + " not found.");
                    return NotFound();
                }

                //_mapper.Map(backlogItem, backlogItemEntity);

                await _functionalityRepository.UpdateAsync(_mapper.Map<Functionality>(functionality));
                await _loggerService.Log(LogLevel.Information, "UpdateFunctionality", "Functionality backlog item with id " + functionality.BacklogItemId + " successfully updated.");
                return Ok(functionality);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateFunctionality", "Error updating functionality backlog item with id " + functionality.BacklogItemId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }



        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("backlog/{backlogId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<List<Functionality>>> GetFunctionalitiesByBacklogId(Guid backlogId)
        {
            var items = await _functionalityRepository.GetFunctionalitiesByBacklogIdAsync(backlogId);

            if (items == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetFunctionalitiesByBacklogId", "Backlog item with id: " + backlogId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetFunctionalitiesByBacklogId", "Backlog item with id: " + backlogId + " successfully returned.");

            List<Functionality> itemsDto = new List<Functionality>();
            foreach (var item in items)
            {
                itemsDto.Add(_mapper.Map<Functionality>(item));
            }
            return Ok(itemsDto);
        }

    }
}
