using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Net.Http.Headers;
using Services;
using Tim_Microservice.Data;
using Tim_Microservice.Models;
using Tim_Microservice.Services;
using Tim_Microservice.VO;

namespace Tim_Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimController : ControllerBase
    {
        private readonly TimService timService;
        private readonly ILoggerService loggerService;
        private readonly IConfiguration configuration;
        private readonly IServiceCall serviceCall;

        public TimController(DatabaseContext databaseContext, ILoggerService loggerService, IConfiguration configuration, IServiceCall serviceCall)
        {
            timService = new TimService(databaseContext);
            this.loggerService = loggerService;
            this.configuration = configuration;
            this.serviceCall = serviceCall;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult< IEnumerable<Tim>>> Get()
        {
            var teams = timService.GetAll();

            if (teams == null || teams.Count() == 0)
            {
                await loggerService.Log(LogLevel.Warning, "Get", "Teams not found.");

                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "Get", "Teams are successfully returned.");

            return Ok(teams);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Tim tim = timService.GetById(id);
            if (tim != null)
            {
                await loggerService.Log(LogLevel.Information, "GetById", "Team with id: " + id + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, tim); 
            }
            await loggerService.Log(LogLevel.Warning, "GetById", "Team item with id: " + id + " not found."); 
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Tim does not exist" });

        }

        [HttpGet("GetByNaziv/{naziv}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByNaziv(string naziv)
        {
            Tim tim = timService.GetByNaziv(naziv);

            if (tim != null)
            {
                await loggerService.Log(LogLevel.Information, "GetByNaziv", "Team with name: " + naziv + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, tim);

            }
            await loggerService.Log(LogLevel.Warning, "GetByNaziv", "Team item with name: " + naziv + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Tim does not exist" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] Tim tim)
        {
            try
            {
                tim.TimID = Guid.NewGuid();
                timService.Save(tim);
                await loggerService.Log(LogLevel.Information, "Post", "Team successfully created.");
                return StatusCode(StatusCodes.Status201Created, tim);
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "Post", "Error creating team. ", ex);
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Tim tim)
        {
            if(id != tim.TimID)
            {
                await loggerService.Log(LogLevel.Warning, "Put", "Team with id " + tim.TimID + " not found.");
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "ID is not valid" });
            }
            try
            {
                timService.Update(tim);
                await loggerService.Log(LogLevel.Information, "Put", "Team with id " + tim.TimID + " successfully updated.");
                return StatusCode(StatusCodes.Status200OK, tim);

            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Warning, "Put", "Team with id " + tim.TimID + " not found.");
                return StatusCode(StatusCodes.Status400BadRequest, ex);

            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Tim tim = timService.GetById(id);
            if(tim == null)
            {
                await loggerService.Log(LogLevel.Warning, "Delete", "Team with id " + id + " not found.");
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Tim does not exist" });

            }
            timService.Delete(tim);
            await loggerService.Log(LogLevel.Information, "Delete", "Team with id " + tim.TimID + " succesfully deleted.");
            return StatusCode(StatusCodes.Status200OK, tim);
        }

        [HttpGet("projekat/{timId}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]


        public async Task<ActionResult<List<Projekat>>> GetProjekatForTim(Guid timId)
        {
            string url = configuration["Services:ProjekatService"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            List<Projekat> projekti = await serviceCall.SendGetRequestAsync(url + "/" + timId, token);

            if (projekti is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetProjekatForTim", "Projekat for tim  with id: " + timId + " successfully returned.");
                return Ok(projekti);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetProjekatForTim", "Projekat for tim  with id: " + timId + " not found");
                return NoContent();
            }

        }

        [HttpGet("clan/{id}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<IActionResult> GetClanoviTimId(Guid id)
        {
            List<ClanTima> clanovi = timService.GetClanoviTimId(id);

            if (clanovi != null)
            {
                await loggerService.Log(LogLevel.Information, "GetClanoviTimId", "Team members for team with id: " + id + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, clanovi);

            }
            await loggerService.Log(LogLevel.Warning, "GetClanoviTimId", "Team members for team with id: " + id + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Team members for team not exist" });
        }

        [HttpGet("username/{username}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<IActionResult> GetTimoviUsername(string username)
        {
            List<Tim> timovi = timService.GetTimoviUsername(username);

            if (timovi != null)
            {
                await loggerService.Log(LogLevel.Information, "GetTimoviUsername", "Teams for username: " + username + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, timovi);

            }
            await loggerService.Log(LogLevel.Warning, "GetTimoviUsername", "Teams  for username: " + username + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Team members for team not exist" });
        }
    }
}
