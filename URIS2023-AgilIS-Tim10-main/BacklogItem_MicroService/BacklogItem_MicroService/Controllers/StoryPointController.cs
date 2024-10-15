using AutoMapper;
using BacklogItem_MicroService.Data.Descriptions;
using BacklogItem_MicroService.Data.StoryPoints;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.DescriptionDTOs;
using BacklogItem_MicroService.Models.DTO.StoryPointDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/storyPoints")]
    [Produces("application/json", "application/xml")]
    public class StoryPointController : ControllerBase
    {
        private readonly IStoryPointRepository _storyPointRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public StoryPointController(IStoryPointRepository storyPointRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _storyPointRepository = storyPointRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<StoryPoint>>> GetAllStoryPoints()
        {

            var points = await _storyPointRepository.GetAllAsync();

            if (points == null || points.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllStoryPoints", "Story point not found.");

                return NoContent();
            }

            Console.WriteLine(await _loggerService.Log(LogLevel.Information, "GetAllStoryPoints", "Story points successfully returned."));
            return Ok(_mapper.Map<List<StoryPointDto>>(points));

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{storyPointId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<StoryPoint>> GetStoryPoint(Guid storyPointId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var point = await _storyPointRepository.GetByIdAsync(storyPointId);

            if (point == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetStoryPoint", "Story point with id: " + storyPointId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetStoryPoint", "Story point with id: " + storyPointId + " successfully returned.");

            return Ok(_mapper.Map<StoryPointDto>(point));
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        [HttpDelete("{storyPointId}")]
        public async Task<IActionResult> DeleteStoryPoint(Guid storyPointId)
        {

            try
            {
                var point = await _storyPointRepository.GetByIdAsync(storyPointId);

                if (point == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "DeleteStoryPoint", "Story point with id " + storyPointId + " not found.");
                    return NotFound();
                }

                await _storyPointRepository.DeleteAsync(storyPointId);
                _loggerService.Log(LogLevel.Information, "DeleteStoryPoint", "Story point with id " + storyPointId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteDescription", "Error deleting description with id " + storyPointId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<StoryPointConfirmationDto>> CreateStoryPoint([FromBody] StoryPointCreationDto storyPoint)
        {
            try
            {
                StoryPoint storyPointEntity = _mapper.Map<StoryPoint>(storyPoint);
                storyPointEntity.StoryPointId = Guid.NewGuid();
                
                StoryPointConfirmation confirmation = await _storyPointRepository.CreateStoryPointAsync(storyPointEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetStoryPoint", "StoryPoint", new { storyPointId = confirmation.StoryPointId });



                await _loggerService.Log(LogLevel.Information, "CreateStoryPoint", "Story point successfully created.");
                return Created(location, _mapper.Map<StoryPointConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                 await _loggerService.Log(LogLevel.Error, "CreateDescription", "Error creating Story point. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<StoryPointDto>> UpdateStoryPoint([FromBody] StoryPointUpdateDto storyPoint)
        {
            try
            {
                var storyPointEntity = await _storyPointRepository.GetByIdAsync(storyPoint.StoryPointId);

                if (storyPointEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateStoryPoint", "Story point with id " + storyPoint.StoryPointId + " not found.");
                    return NotFound();
                }



                await _storyPointRepository.UpdateAsync(_mapper.Map<StoryPoint>(storyPoint));
                await _loggerService.Log(LogLevel.Information, "UpdateStoryPoint", "Story point with id " + storyPoint.StoryPointId + " successfully updated.");
                return Ok(storyPoint);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateStoryPoint", "Error updating story point with id " + storyPoint.StoryPointId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }

        }
    }
}
