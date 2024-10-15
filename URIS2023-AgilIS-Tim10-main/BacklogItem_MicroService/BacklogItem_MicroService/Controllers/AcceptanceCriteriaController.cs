using AutoMapper;
using BacklogItem_MicroService.Data.AcceptanceCriterias;
using BacklogItem_MicroService.Data.BacklogItems;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.AcceptanceCriteriaDTOs;
using BacklogItem_MicroService.Models.DTO.BacklogItemDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/acceptanceCriteria")]
    [Produces("application/json", "application/xml")]
    public class AcceptanceCriteriaController : ControllerBase
    {
        private readonly IAcceptanceCriteriaRepository _acceptanceCriteriaRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public AcceptanceCriteriaController(IAcceptanceCriteriaRepository acceptanceCriteriaRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _acceptanceCriteriaRepository = acceptanceCriteriaRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<AcceptanceCriteria>>> GetAllAcceptanceCriterias()
        { 

            var criterias = await _acceptanceCriteriaRepository.GetAllAsync();

            if (criterias == null || criterias.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllAcceptanceCriterias", "Acceptance criterias not found.");

                return NoContent();
            }

             Console.WriteLine(await _loggerService.Log(LogLevel.Information, "GetAllAcceptanceCriterias", "Acceptance criterias successfully returned."));
            return Ok(_mapper.Map<List<AcceptanceCriteriaDto>>(criterias));

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{acceptanceCriteriaId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<AcceptanceCriteria>> GetAcceptanceCriteria(Guid acceptanceCriteriaId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var criteria = await _acceptanceCriteriaRepository.GetByIdAsync(acceptanceCriteriaId);

            if (criteria == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAcceptanceCriteria", "Acceptance criteria with id: " + acceptanceCriteriaId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetAcceptanceCriteria", "Acceptance criteria with id: " + acceptanceCriteriaId + " successfully returned.");

            return Ok(_mapper.Map<AcceptanceCriteriaDto>(criteria));
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        [HttpDelete("{acceptanceCriteriaId}")]
        public async Task<IActionResult> DeleteAcceptanceCriteria(Guid acceptanceCriteriaId)
        {

            try
            {
                var criteria = await _acceptanceCriteriaRepository.GetByIdAsync(acceptanceCriteriaId);

                if (criteria == null)
                {
                     await _loggerService.Log(LogLevel.Warning, "DeleteAcceptanceCriteria", "Acceptance criteria with id " + acceptanceCriteriaId + " not found.");
                    return NotFound();
                }

                await _acceptanceCriteriaRepository.DeleteAsync(acceptanceCriteriaId);
                _loggerService.Log(LogLevel.Information, "DeleteAcceptanceCriteria", "Acceptance criteria with id " + acceptanceCriteriaId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteAcceptanceCriteria", "Error deleting acceptance criteria with id " +  acceptanceCriteriaId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<AcceptanceCriteriaConfirmationDto>> CreateAcceptanceCriteria([FromBody] AcceptanceCriteriaCreationDto acceptanceCriteria)
        {
            try
            {
                AcceptanceCriteria acceptanceCriteriaEntity = _mapper.Map<AcceptanceCriteria>(acceptanceCriteria);
                acceptanceCriteriaEntity.AcceptanceCriteriaId = Guid.NewGuid();
                Console.WriteLine(acceptanceCriteriaEntity);
                AcceptanceCriteriaConfirmation confirmation = await _acceptanceCriteriaRepository.CreateAcceptanceCriteriaAsync(acceptanceCriteriaEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetAcceptanceCriteria", "AcceptanceCriteria", new { acceptanceCriteriaId = confirmation.BacklogItemId });



                 await _loggerService.Log(LogLevel.Information, "CreateAcceptanceCriteria", "Acceptance criteria successfully created.");
                return Created(location, _mapper.Map<AcceptanceCriteriaConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                 await _loggerService.Log(LogLevel.Error, "CreateAcceptanceCriteria", "Error creating acceptance criteria. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<AcceptanceCriteriaDto>> UpdateAcceptanceCriteria([FromBody] AcceptanceCriteriaUpdateDto acceptanceCriteria)
        {
            try
            {
                var acceptanceCriteriaEntity = await _acceptanceCriteriaRepository.GetByIdAsync(acceptanceCriteria.AcceptanceCriteriaId);

                if (acceptanceCriteriaEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateAcceptanceCriteria", "Acceptance criteria with id " + acceptanceCriteria.AcceptanceCriteriaId + " not found.");
                    return NotFound();
                }
                
               

                await _acceptanceCriteriaRepository.UpdateAsync(_mapper.Map<AcceptanceCriteria>(acceptanceCriteria));
                await _loggerService.Log(LogLevel.Information, "UpdateAcceptanceCriteria", "Acceptance criteria with id " + acceptanceCriteria.AcceptanceCriteriaId + " successfully updated.");
                return Ok(acceptanceCriteria);
            }
            catch (Exception e)
            { 
                await _loggerService.Log(LogLevel.Error, "UpdateAcceptanceCriteria", "Error updating acceptance criteria with id " + acceptanceCriteria.AcceptanceCriteriaId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }

        }
    }
}
