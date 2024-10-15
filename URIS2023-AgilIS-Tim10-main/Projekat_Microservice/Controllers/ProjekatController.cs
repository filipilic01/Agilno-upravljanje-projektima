using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Projekat_Microservice.Data;
using Projekat_Microservice.Models;
using Projekat_Microservice.Models.ProjekatDto;
using Projekat_Microservice.Services;

namespace Projekat_Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjekatController : ControllerBase
    {
        private readonly ProjekatService projekatService;
        private readonly ILoggerService loggerService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IServiceCall serviceCall;

        public ProjekatController(DatabaseContext databaseContext, ILoggerService loggerService, IMapper mapper,IConfiguration configuration, IServiceCall serviceCall)
        {
            this.projekatService = new ProjekatService(databaseContext);
            this.loggerService = loggerService;
            this.mapper = mapper;
            this.configuration = configuration;
            this.serviceCall = serviceCall;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<IEnumerable<Projekat>>> Get()
        {
            var projects = projekatService.GetAll();

            if (projects == null || projects.Count() == 0)
            {
                await loggerService.Log(LogLevel.Warning, "Get", "Projects not found.");

                return NoContent();
            }

            await loggerService.Log(LogLevel.Information, "Get", "Projects successfully returned.");

            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Projekat projekat = projekatService.GetById(id);

            if (projekat != null)
            {
                await loggerService.Log(LogLevel.Information, "GetById", "Project with id: " + id + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, projekat);
            }
            await loggerService.Log(LogLevel.Warning, "GetById", "Project item with id: " + id + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Project does not exist" });

        }

        [HttpGet("GetByNaziv/{naziv}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<IActionResult> GetByNaziv(string naziv)
        {
            Projekat projekat = projekatService.GetByNaziv(naziv);

            if (projekat != null)
            {
                await loggerService.Log(LogLevel.Information, "GetByNaziv", "Project with name: " + naziv + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, projekat);

            }
            await loggerService.Log(LogLevel.Warning, "GetByNaziv", "Project item with name: " + naziv + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Project does not exist" });
        }

        [HttpPost]
        [Authorize(Roles = "ScrumMaster, Admin")]
        public async Task<IActionResult> Post([FromBody] Projekat projekat)
        {
            try
            {
                projekat.ProjekatID = Guid.NewGuid();
                projekatService.Save(projekat);
                await loggerService.Log(LogLevel.Information, "Post", "Project successfully created.");
                return StatusCode(StatusCodes.Status201Created, projekat);
            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Error, "Post", "Error creating project. ", ex);
                return StatusCode(StatusCodes.Status400BadRequest, ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ScrumMaster, Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Projekat projekat)
        {
            if (id != projekat.ProjekatID)
            {
                await loggerService.Log(LogLevel.Warning, "Put", "Project with id " + projekat.ProjekatID + " not found.");
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "ID is not valid" });
            }
            try
            {
                projekatService.Update(projekat);
                await loggerService.Log(LogLevel.Information, "Put", "Project with id " + projekat.ProjekatID + " successfully updated.");
                return StatusCode(StatusCodes.Status200OK, projekat);

            }
            catch (Exception ex)
            {
                await loggerService.Log(LogLevel.Warning, "Put", "Project with id " + projekat.ProjekatID + " not found.");
                return StatusCode(StatusCodes.Status400BadRequest, ex);

            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ScrumMaster, Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Projekat projekat = projekatService.GetById(id);
            if (projekat == null)
            {
                await loggerService.Log(LogLevel.Warning, "Delete", "Project with id " + id + " not found.");
                return StatusCode(StatusCodes.Status404NotFound, new { message = "Project does not exist" });

            }

            projekatService.Delete(projekat);
            await loggerService.Log(LogLevel.Information, "Delete", "Project with id " + projekat.ProjekatID + " succesfully deleted.");
            return StatusCode(StatusCodes.Status200OK, projekat);
        }

        [HttpGet("GetByTimId/{id}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<IActionResult> GetByTimId(Guid id )
        {
           List<Projekat> projekti = projekatService.GetByTimId(id);

            if (projekti != null)
            {
                await loggerService.Log(LogLevel.Information, "GetByNaziv", "Project with name: " + id + " successfully returned.");
                return StatusCode(StatusCodes.Status200OK, projekti);

            }
            await loggerService.Log(LogLevel.Warning, "GetByNaziv", "Project item with name: " + id + " not found.");
            return StatusCode(StatusCodes.Status404NotFound, new { message = "Project does not exist" });
        }

        [HttpGet("backlog/{projekatId}")]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<BacklogDto>>> GetBacklogsForProjekat(Guid projekatId)
        {
            string url = configuration["Services:BacklogService"];
            Console.WriteLine(url);
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            var backlogtDto = await serviceCall.SendGetRequestAsync(url + "/" + projekatId, token);

            if (backlogtDto is not null)
            {
                await loggerService.Log(LogLevel.Information, "GetBacklogsForProjekat", "Backlogs for projekat with id: " + projekatId + " successfully returned.");
                return Ok(backlogtDto);
            }
            else
            {
                await loggerService.Log(LogLevel.Warning, "GetBacklogsForProjekat", "Backlogs for projekat  with id: " + projekatId + " not found");
                return NoContent();
            }

        }
    }
}

