using Logger_MicroService.Data;
using Logger_MicroService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Logger_MicroService.Controllers
{
    [Route("api/logger")]
    [ApiController]
    
    public class LoggerController : ControllerBase
    {
        private static ILoggerManager _logger;
       

        public LoggerController(ILoggerManager logger)
        {
            _logger = logger;
           
        }

        
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PostLogMessage([FromBody] Log log)
        {
            
            try
            {
                string message = $"{log.Service} |-> {log.Method} |-> {log.Message}.";

                switch (log.Level)
                {
                    case LogLevel.Information:
                        _logger.LogInformation(message);
                        break;

                    case LogLevel.Error:
                        _logger.LogError(log.Error, message);
                        break;

                    case LogLevel.Debug:
                        _logger.LogDebug(message);
                        break;

                    case LogLevel.Warning:
                        _logger.LogWarning(message);
                        break;

                    default:

                        break;
                }

                        //_logger.LogDebug(message);


                        return Ok(Task.FromResult("Message was successfully enrolled."));
            }
            catch (Exception e) 
            {
                _logger.LogError(e, "Error during enroll in log file.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error during enroll in log file.");
            }
            
        }


      
    }
}
