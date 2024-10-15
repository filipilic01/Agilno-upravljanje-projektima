using AutoMapper;
using BacklogItem_MicroService.Data.BacklogItems;
using BacklogItem_MicroService.Data.Epics;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.DTO.EpicDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/epics")]
    [Produces("application/json", "application/xml")]
    public class EpicController : ControllerBase
    {
        private readonly IEpicRepository _epicRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public EpicController(IEpicRepository epicRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _epicRepository = epicRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<Epic>>> GetAllEpics()
        {
            var items = await _epicRepository.GetAllAsync();

            if (items == null || items.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllEpics", "Epic backlog items not found.");

                return NoContent();
            }

            await _loggerService.Log(LogLevel.Information, "GetAllEpics", "Epic backlog items successfully returned.");
            return Ok(_mapper.Map<List<EpicDto>>(items));

        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<Epic>> GetEpic(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var item = await _epicRepository.GetByIdAsync(backlogItemId);

            if (item == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetEpic", "Epic backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetEpic", "Epic backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<EpicDto>(item));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{backlogItemId}")]
        public async Task<IActionResult> DeleteEpic(Guid backlogItemId)
        {

            try
            {
                var item = await _epicRepository.GetByIdAsync(backlogItemId);

                if (item == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "DeleteEpic", "Epic backlog item with id " + backlogItemId + " not found.");
                    return NotFound();
                }

                await _epicRepository.DeleteAsync(backlogItemId);
                //_loggerService.Log(LogLevel.Information, "DeleteEpic", "Epic backlog item with id " + backlogItemId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteEpic", "Error deleting epic backlog item with id " +  backlogItemId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<EpicConfirmationDto>> CreateEpic([FromBody] EpicCreationDto epic)
        {
            try
            {
                Epic epicEntity = _mapper.Map<Epic>(epic);
                epicEntity.BacklogItemId = Guid.NewGuid();
                Console.WriteLine(epicEntity);
                EpicConfirmation confirmation = await _epicRepository.CreateEpicAsync(epicEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetEpic", "Epic", new { backlogItemId = confirmation.BacklogItemId });



                 await _loggerService.Log(LogLevel.Information, "CreateEpic", "Epic backlog item successfully created.");
                return Created(location, _mapper.Map<EpicConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "CreateEpic", "Error creating epic backlog item. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<EpicDto>> UpdateEpic([FromBody] EpicUpdateDto epic)
        {
            try
            {
                var epicEntity = await _epicRepository.GetByIdAsync(epic.BacklogItemId);

                if (epicEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateEpic", "Epic backlog item with id " + epic.BacklogItemId + " not found.");
                    return NotFound();
                }

                //_mapper.Map(backlogItem, backlogItemEntity);

                await _epicRepository.UpdateAsync(_mapper.Map<Epic>(epic));
                await _loggerService.Log(LogLevel.Information, "UpdateEpic", "Epic backlog item with id " + epic.BacklogItemId + " successfully updated.");
                return Ok(epic);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateEpic", "Error updating epic backlog item with id " + epic.BacklogItemId + ".",e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }



        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("backlog/{backlogId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<List<Epic>>> GetEpicsByBacklogId(Guid backlogId)
        {
            var items = await _epicRepository.GetEpicsByBacklogIdAsync(backlogId);

            if (items == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetEpicsByBacklogId", "Backlog item with id: " + backlogId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetEpicsByBacklogId", "Backlog item with id: " + backlogId + " successfully returned.");

            List<Epic> itemsDto = new List<Epic>();
            foreach (var item in items)
            {
                itemsDto.Add(_mapper.Map<Epic>(item));
            }
            return Ok(itemsDto);
        }
    }
}
