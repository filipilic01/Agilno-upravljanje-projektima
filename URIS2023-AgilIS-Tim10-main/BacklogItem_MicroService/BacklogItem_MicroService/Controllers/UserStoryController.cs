using AutoMapper;
using BacklogItem_MicroService.Data.Epics;
using BacklogItem_MicroService.Data.UserStories;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.DTO.EpicDTOs;
using BacklogItem_MicroService.Models.DTO.UserStoryDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/userStories")]
    [Produces("application/json", "application/xml")]
    public class UserStoryController : ControllerBase
    {
        private readonly IUserStoryRepository _userStoryRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public UserStoryController(IUserStoryRepository userStoryRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _userStoryRepository = userStoryRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<UserStory>>> GetAllUserStories()
        {
            var items = await _userStoryRepository.GetAllAsync();

            if (items == null || items.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllUserStories", "User story backlog items not found.");

                return NoContent();
            }

            await _loggerService.Log(LogLevel.Information, "GetAllUserStories", "User story backlog items successfully returned.");
            return Ok(_mapper.Map<List<UserStoryDto>>(items));

        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<UserStory>> GetUserStory(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var item = await _userStoryRepository.GetByIdAsync(backlogItemId);

            if (item == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetUserStory", "User story backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetUserStory", "User story backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<UserStoryDto>(item));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{backlogItemId}")]
        public async Task<IActionResult> DeleteUserStory(Guid backlogItemId)
        {

            try
            {
                var item = await _userStoryRepository.GetByIdAsync(backlogItemId);

                if (item == null)
                {
                     await _loggerService.Log(LogLevel.Warning, "DeleteUserStory", "User story backlog item with id " + backlogItemId + " not found.");
                    return NotFound();
                }

                await _userStoryRepository.DeleteAsync(backlogItemId);
                await _loggerService.Log(LogLevel.Information, "DeleteUserStory", "User story backlog item with id " + backlogItemId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteUserStory", "Error deleting user story backlog item with id " +  backlogItemId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<UserStoryConfirmationDto>> CreateUserStory([FromBody] UserStoryCreationDto userStory)
        {
            try
            {
                UserStory userStoryEntity = _mapper.Map<UserStory>(userStory);
                userStoryEntity.BacklogItemId = Guid.NewGuid();
                Console.WriteLine(userStoryEntity);
                UserStoryConfirmation confirmation = await _userStoryRepository.CreateUserStoryAsync(userStoryEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetUserStory", "UserStory", new { backlogItemId = confirmation.BacklogItemId });



                 await _loggerService.Log(LogLevel.Information, "CreateUserStory", "User story backlog item successfully created.");
                return Created(location, _mapper.Map<UserStoryConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                 await _loggerService.Log(LogLevel.Error, "CreateUserStory", "Error creating user story backlog item. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<UserStoryDto>> UpdateUserStory([FromBody] UserStoryUpdateDto userStory)
        {
            try
            {
                var userStoryEntity = await _userStoryRepository.GetByIdAsync(userStory.BacklogItemId);

                if (userStoryEntity == null)
                {
                    //await _loggerService.Log(LogLevel.Warning, "UpdateUserStory", "User story backlog item with id " + userStory.BacklogItemId + " not found.");
                    return NotFound();
                }

                //_mapper.Map(backlogItem, backlogItemEntity);

                await _userStoryRepository.UpdateAsync(_mapper.Map<UserStory>(userStory));
                await _loggerService.Log(LogLevel.Information, "UpdateUserStory", "User story backlog item with id " + userStory.BacklogItemId + " successfully updated.");
                return Ok(userStory);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateUserStory", "Error updating user story backlog item with id " + userStory.BacklogItemId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }



        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("backlog/{backlogId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<List<UserStory>>> GetUserStoriesByBacklogId(Guid backlogId)
        {
            var items = await _userStoryRepository.GetUserStoriesByBacklogIdAsync(backlogId);

            if (items == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetUserStoriesByBacklogId", "Backlog item with id: " + backlogId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetUserStoriesByBacklogId", "Backlog item with id: " + backlogId + " successfully returned.");

            List<UserStory> itemsDto = new List<UserStory>();
            foreach (var item in items)
            {
                itemsDto.Add(_mapper.Map<UserStory>(item));
            }
            return Ok(itemsDto);
        }
    }
}
