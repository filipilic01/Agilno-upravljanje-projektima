using AutoMapper;
using BacklogItem_MicroService.Data.BacklogItems;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.DTO.DescriptionDTOs;
using BacklogItem_MicroService.Models.DTO.StoryPointDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Models.OtherModelServices;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System.Globalization;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems")]
    [Produces("application/json", "application/xml")]

    public class BacklogItemController : ControllerBase
    {
        private readonly IBacklogItemRepository _backlogItemRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;
        private readonly IServiceCall<RagDto> _ragService;
        private readonly IServiceCall<StatusDto> _statusService;
        private readonly IConfiguration _configuration;

        public BacklogItemController(IBacklogItemRepository backlogItemRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService, IServiceCall<RagDto> ragService, IServiceCall<StatusDto> statusService, IConfiguration configuration)
        {
            _backlogItemRepository = backlogItemRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
            _ragService = ragService;
            _statusService = statusService;
            _configuration = configuration;
        }


        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogItem>>> GetAllBacklogItems()
        {
            var items = await _backlogItemRepository.GetAllAsync();

            if (items == null || items.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllBacklogItems", "Backlog items not found.");

                return NoContent();
            }

            Console.WriteLine(await _loggerService.Log(LogLevel.Information, "GetAllBacklogItems", "Backlog items successfully returned."));
            return Ok(items);

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<BacklogItem>> GetBacklogItem(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var item = await _backlogItemRepository.GetByIdAsync(backlogItemId);

            if (item == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetBacklogItem", "Backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetBacklogItem", "Backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<BacklogItemDto>(item));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{backlogItemId}")]
        public async Task<IActionResult> DeleteBacklogItem(Guid backlogItemId)
        {

            try
            {
                var item = await _backlogItemRepository.GetByIdAsync(backlogItemId);

                if (item == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "DeleteBacklogItem", "Backlog item with id " + backlogItemId + " not found.");
                    return NotFound();
                }

                await _backlogItemRepository.DeleteAsync(backlogItemId);
                _loggerService.Log(LogLevel.Information, "DeleteBacklogItem", "Backlog item with id " + backlogItemId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteBacklogItem", "Error deleting backlog item with id " + backlogItemId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<BacklogItemConfirmationDto>> CreateBacklogItem([FromBody] BacklogItemCreationDto backlogItem)
        {
            try
            {
                BacklogItem backlogItemEntity = _mapper.Map<BacklogItem>(backlogItem);
                backlogItemEntity.BacklogItemId = Guid.NewGuid();
                Console.WriteLine(backlogItemEntity);
                BacklogItemConfirmation confirmation = await _backlogItemRepository.CreateBacklogItemAsync(backlogItemEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetBacklogItem", "BacklogItem", new { backlogItemId = confirmation.BacklogItemId });



                await _loggerService.Log(LogLevel.Information, "CreateBacklogItem", "Backlog item successfully created.");
                return Created(location, _mapper.Map<BacklogItemConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "CreateBacklogItem", "Error creating backlog item. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<BacklogItemDto>> UpdateBacklogItem([FromBody] BacklogItemUpdateDto backlogItem)
        {
            try
            {
                var backlogItemEntity = await _backlogItemRepository.GetByIdAsync(backlogItem.BacklogItemId);

                if (backlogItemEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateBacklogItem", "Backlog item with id " + backlogItem.BacklogItemId + " not found.");
                    return NotFound();
                }

                //_mapper.Map(backlogItem, backlogItemEntity);

                await _backlogItemRepository.UpdateAsync(_mapper.Map<BacklogItem>(backlogItem));
                await _loggerService.Log(LogLevel.Information, "UpdateBacklogItem", "Backlog item with id " + backlogItem.BacklogItemId + " successfully updated.");
                return Ok(backlogItem);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateBacklogItem", "Error updating backlog item with id " + backlogItem.BacklogItemId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("criteria/{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<AcceptanceCriteria>> GetAcceptanceCriteriaByBacklogItemId(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var criteria = await _backlogItemRepository.GetAcceptanceCriteriaByBacklogItemIdAsync(backlogItemId);

            if (criteria == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAcceptanceCriteriaByBacklogItemId", "Backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetAcceptanceCriteriaByBacklogItemId", "Backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<AcceptanceCriteriaDto>(criteria));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("description/{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<Description>> GetDescriptionByBacklogItemId(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var desc = await _backlogItemRepository.GetDescriptionByBacklogItemIdAsync(backlogItemId);

            if (desc == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetDescriptionByBacklogItemId", "Backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetDescriptionByBacklogItemId", "Backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<DescriptionDto>(desc));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("storyPoint/{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<StoryPoint>> GetStoryPointByBacklogItemId(Guid backlogItemId) 
        {
            var point = await _backlogItemRepository.GetStoryPointByBacklogItemIdAsync(backlogItemId);

            if (point == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetStoryPointByBacklogItemId", "Backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetStoryPointByBacklogItemId", "Backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<StoryPointDto>(point));
        }

        
        [HttpGet("rag/{backlogItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<RagDto>>GetRagForBacklogItem(Guid backlogItemId)
        {
            string url = _configuration["Services:RagService"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var ragDto = await _ragService.SendGetRequestAsync(url + "/" +backlogItemId, token);

            if (ragDto is not null)
            {
                await _loggerService.Log(LogLevel.Information, "GetRagForBacklogItem", "Rag list for backlog item with id: " + backlogItemId + " successfully returned.");
                return Ok(ragDto);
            }
            else
            {
                await _loggerService.Log(LogLevel.Warning, "GetRagForBacklogItem", "Rag list for backlog item with id: " + backlogItemId + " not found");
                return NoContent();
            }

        }

        [HttpGet("status/{backlogItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<StatusDto>> GetStatusForBacklogItem(Guid backlogItemId)
        {
            string url = _configuration["Services:StatusService"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var statusDto = await _statusService.SendGetRequestAsync(url + "/" + backlogItemId, token);

            if (statusDto is not null)
            {
                await _loggerService.Log(LogLevel.Information, "GetStatusForBacklogItem", "Status for backlog item with id: " + backlogItemId + " successfully returned.");
                Console.WriteLine(statusDto);
                return Ok(statusDto);
            }
            else
            {
                await _loggerService.Log(LogLevel.Warning, "GetStatusForBacklogItem", "Status for backlog item with id: " + backlogItemId + " not found");
                return NoContent();
            }

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("backlog/{backlogId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<List<BacklogItem>>> GetBacklogItemByBacklogId(Guid backlogId)
        {
            var items = await _backlogItemRepository.GetBacklogItemsByBacklogIdAsync(backlogId);

            if (items == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetBacklogsItemByBacklogId", "Backlog item with id: " + backlogId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetBacklogsItemByBacklogId", "Backlog item with id: " + backlogId + " successfully returned.");

           /* List<BacklogItemDto> itemsDto = new List<BacklogItemDto>();
            foreach(var item in items)
            {
                itemsDto.Add(_mapper.Map<BacklogItemDto>(item));
            }*/
            return Ok(items);
        }


    }
}
