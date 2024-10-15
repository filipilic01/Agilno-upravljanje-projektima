using AutoMapper;
using BacklogItem_MicroService.Data.Tasks;
using BacklogItem_MicroService.Data.UserStories;
using BacklogItem_MicroService.Models.Confirmations;
using BacklogItem_MicroService.Models.DTO.TaskDTOs;
using BacklogItem_MicroService.Models.DTO.UserStoryDTOs;
using BacklogItem_MicroService.Models.Entities;
using BacklogItem_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BacklogItem_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogItems/tasks")]
    [Produces("application/json", "application/xml")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;
        private readonly ILoggerService _loggerService;

        public TaskController(ITaskRepository taskRepository, IMapper mapper, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
            _loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        public async Task<ActionResult<List<TaskEntity>>> GetAllTasks()
        {
            var items = await _taskRepository.GetAllAsync();

            if (items == null || items.Count == 0)
            {
                await _loggerService.Log(LogLevel.Warning, "GetAllTasks", "Task backlog items not found.");

                return NoContent();
            }

            await _loggerService.Log(LogLevel.Information, "GetAllTasks", "Task backlog items successfully returned.");
            return Ok(_mapper.Map<List<TaskDto>>(items));

        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{backlogItemId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<TaskEntity>> GetTask(Guid backlogItemId) //Na ovaj parametar će se mapirati ono što je prosleđeno u ruti
        {
            var item = await _taskRepository.GetByIdAsync(backlogItemId);

            if (item == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetTask", "Task backlog item with id: " + backlogItemId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetTask", "Task backlog item with id: " + backlogItemId + " successfully returned.");

            return Ok(_mapper.Map<TaskDto>(item));
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpDelete("{backlogItemId}")]
        public async Task<IActionResult> DeleteTask(Guid backlogItemId)
        {

            try
            {
                var item = await _taskRepository.GetByIdAsync(backlogItemId);

                if (item == null)
                {
                     await _loggerService.Log(LogLevel.Warning, "DeleteTask", "Task backlog item with id " + backlogItemId + " not found.");
                    return NotFound();
                }

                await _taskRepository.DeleteAsync(backlogItemId);
                await _loggerService.Log(LogLevel.Information, "DeleteTask", "Task backlog item with id " + backlogItemId + " succesfully deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                await _loggerService.Log(LogLevel.Error, "DeleteTask", "Error deleting task backlog item with id " +  backlogItemId + ".", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete error");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskConfirmationDto>> CreateTask([FromBody] TaskCreationDto task)
        {
            try
            {
                TaskEntity taskEntity = _mapper.Map<TaskEntity>(task);
                taskEntity.BacklogItemId = Guid.NewGuid();
                Console.WriteLine(taskEntity);
                TaskConfirmation confirmation = await _taskRepository.CreateTaskAsync(taskEntity);


                //Generisati identifikator novokreiranog resursa
                string location = _linkGenerator.GetPathByAction("GetTask", "Task", new { backlogItemId = confirmation.BacklogItemId });



                await _loggerService.Log(LogLevel.Information, "CreateTask", "Task backlog item successfully created.");
                return Created(location, _mapper.Map<TaskConfirmationDto>(confirmation));

            }
            catch (Exception ex)
            {
                 await _loggerService.Log(LogLevel.Error, "CreateTask", "Error creating task backlog item. ", ex);

                return StatusCode(StatusCodes.Status500InternalServerError, "Create error");
            }
        }

        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskDto>> UpdateTask([FromBody] TaskUpdateDto task)
        {
            try
            {
                var taskEntity = await _taskRepository.GetByIdAsync(task.BacklogItemId);

                if (taskEntity == null)
                {
                    await _loggerService.Log(LogLevel.Warning, "UpdateTask", "Task backlog item with id " + task.BacklogItemId + " not found.");
                    return NotFound();
                }

                //_mapper.Map(backlogItem, backlogItemEntity);

                await _taskRepository.UpdateAsync(_mapper.Map<TaskEntity>(task));
                await _loggerService.Log(LogLevel.Information, "UpdateTask", "Task backlog item with id " + task.BacklogItemId + " successfully updated.");
                return Ok(task);
            }
            catch (Exception e)
            {
                await _loggerService.Log(LogLevel.Error, "UpdateTask", "Error updating task backlog item with id " + task.BacklogItemId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }



        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("backlog/{backlogId}")] //Dodatak na rutu koja je definisana na nivou kontrolera
        public async Task<ActionResult<List<TaskEntity>>> GetTasksByBacklogId(Guid backlogId)
        {
            var items = await _taskRepository.GetTasksByBacklogIdAsync(backlogId);

            if (items == null)
            {
                await _loggerService.Log(LogLevel.Warning, "GetTasksByBacklogId", "Backlog item with id: " + backlogId + " not found.");
                return NotFound();
            }

            await _loggerService.Log(LogLevel.Information, "GetTasksByBacklogId", "Backlog item with id: " + backlogId + " successfully returned.");

            List<TaskEntity> itemsDto = new List<TaskEntity>();
            foreach (var item in items)
            {
                itemsDto.Add(_mapper.Map<TaskEntity>(item));
            }
            return Ok(itemsDto);
        }
    }
}
