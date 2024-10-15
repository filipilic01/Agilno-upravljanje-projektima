using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rag_MicroService.Data;
using Rag_MicroService.Models;
using Rag_MicroService.Models.DTO;
using Rag_MicroService.Models.Entities;
using Rag_MicroService.Services;

namespace Rag_MicroService.Controllers
{
    [ApiController]
    [Route("api/rag")]
    [Produces("application/json", "application/xml")]
    public class RagController : ControllerBase
    {
        private readonly IRagRepository ragRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ILoggerService loggerService;

        public RagController(IRagRepository ragRepository, LinkGenerator linkGenerator, IMapper mapper, ILoggerService loggerService)
        {
            this.ragRepository = ragRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public ActionResult<List<Rag>> GetRags()
        {
            var rags = ragRepository.GetRags();

            if (rags == null || rags.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetRags", "Rags not found.");
                return NoContent();

            }
            Console.WriteLine(loggerService.Log(LogLevel.Information, "GetRags", "Rags successfully returned."));
            return Ok(rags);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{ragId}")]
        public ActionResult<RagDto> GetRagById(Guid ragId)
        {
            var rag = ragRepository.GetRagById(ragId);

            if (rag == null)
            {
                loggerService.Log(LogLevel.Warning, "GetRagById", "Rag with id: " + ragId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetRagById", "Rag with id: " + ragId + " successfully returned.");
            return Ok(mapper.Map<RagDto>(rag));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,ScrumMaster")]
        public ActionResult<RagDto> CreateRag([FromBody] RagCreationDto rag)
        {
            try
            {
                Rag ragEntity = mapper.Map<Rag>(rag);
                ragEntity.RagId = Guid.NewGuid();
                RagConfirmation confirmation = ragRepository.CreateRag(ragEntity);

                //string location = linkGenerator.GetPathByAction("GetRag", "Rag", new { RagId = confirmation.RagConfirmationId });
                loggerService.Log(LogLevel.Information, "CreateRag", "Rag successfully created.");
                return Ok( mapper.Map<RagConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreateRag", "Error creating rag. ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,ScrumMaster")]
        public ActionResult<RagDto> UpdateRag(RagUpdateDto rag)
        {
            try
            {
                var ragEntity = ragRepository.GetRagById(rag.RagId);

                if (ragEntity == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateRag", "Rag with id " + rag.RagId + " not found.");
                    return NotFound();
                }

                ragRepository.UpdateRag(mapper.Map<Rag>(rag));
                loggerService.Log(LogLevel.Information, "UpdateRag", "Rag with id " + rag.RagId + " successfully updated.");
                return Ok(mapper.Map<RagConfirmationDto>(rag));
            }
            catch (Exception e)
            {
                loggerService.Log(LogLevel.Error, "UpdateRag", "Error updating rag with id " + rag.RagId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ScrumMaster")]
        [HttpDelete("{ragId}")]
        public IActionResult DeleteRag(Guid ragId)
        {
            try
            {
                var rag = ragRepository.GetRagById(ragId);

                if (rag == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteRag", "Rag with id " + ragId + " not found.");
                    return NotFound();
                }

                ragRepository.DeleteRag(ragId);
                loggerService.Log(LogLevel.Information, "DeleteRag", "Rag with id " + ragId + " succesfully deleted.");
                return NoContent();
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "DeleteRag", "Error deleting rag with id " + ragId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("backlogItem/{backlogItemId}")]
        public ActionResult<Rag> GetRagByBacklogItemId(Guid backlogItemId)
        {
            var rag = ragRepository.GetRagByBacklogItemId(backlogItemId);

            if (rag == null)
            {
                loggerService.Log(LogLevel.Warning, "GetRagById", "Backlog item with id: " + backlogItemId + " not found.");

                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetRagById", "Backlog item with id: " + backlogItemId + " successfully returned.");
            Console.WriteLine(rag);
            return Ok(rag);
        }

    }
}
