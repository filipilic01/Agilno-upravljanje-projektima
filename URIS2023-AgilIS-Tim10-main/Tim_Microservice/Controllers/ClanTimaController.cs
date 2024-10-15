using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Tim_Microservice.Data;
using Tim_Microservice.Models;
using Tim_Microservice.Services;
using Tim_Microservice.VO;

namespace Tim_Microservice.Controllers
{
    [ApiController]
    [Route("api/Tim/[controller]")]
    public class ClanTimaController : ControllerBase
    {
        private readonly ClanTimaService clanTimaService;
        private readonly ILoggerService loggerService;
        private readonly IMapper mapper;
        public ClanTimaController(DatabaseContext databaseContext, ILoggerService loggerService, IMapper mapper)
        {
            clanTimaService = new ClanTimaService(databaseContext);
            this.loggerService = loggerService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ClanTima>>> Get()
        {
            var members = clanTimaService.GetAll();

            if (members == null || members.Count() == 0)
            {
                await loggerService.Log(LogLevel.Warning, "Get", "Team members not found.");

                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "Get", "Team members are successfully returned.");

            return Ok(members);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            ClanTima clan = clanTimaService.GetById(id);
            if (clan != null)
            {
                await loggerService.Log(LogLevel.Information, "GetById", "Team member with id: " + id + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, clan);
            }
            await loggerService.Log(LogLevel.Warning, "GetById", "Team member item with id: " + id + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Tim member does not exist" });

        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] ClanTimaCreationDto clanDto)
        {
            try
            {
                ClanTima clan = mapper.Map<ClanTima>(clanDto);
                clan.ClanTimaId = Guid.NewGuid();
                clanTimaService.Save(clan);
                await loggerService.Log(LogLevel.Information, "Post", "Team member successfully created.");
                return StatusCode(StatusCodes.Status201Created, clan);
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "Post", "Error creating team member. ", ex);
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ClanTima clan)
        {
            if (id != clan.ClanTimaId)
            {
                await loggerService.Log(LogLevel.Warning, "Put", "Team member with id " + clan.ClanTimaId + " not found.");
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "ID is not valid" });
            }
            try
            {
                clanTimaService.Update(clan);
                await loggerService.Log(LogLevel.Information, "Put", "Team member with id " + clan.ClanTimaId + " successfully updated.");
                return StatusCode(StatusCodes.Status200OK, clan);

            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Warning, "Put", "Team with id " + clan.ClanTimaId + " not found.");
                return StatusCode(StatusCodes.Status400BadRequest, ex);

            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            ClanTima clan = clanTimaService.GetById(id);
            if (clan == null)
            {
                await loggerService.Log(LogLevel.Warning, "Delete", "Team member with id " + id + " not found.");
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Tim does not exist" });

            }
            clanTimaService.Delete(clan);
            await loggerService.Log(LogLevel.Information, "Delete", "Team member with id " + clan.ClanTimaId + " succesfully deleted.");
            return StatusCode(StatusCodes.Status200OK, clan);
        }
    }
}
