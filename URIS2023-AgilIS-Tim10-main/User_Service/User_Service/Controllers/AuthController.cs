using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User_Service.Auth;
using User_Service.Data;
using User_Service.Entities;
using User_Service.Models;
using User_Service.Services;

namespace User_Service.Controllers
{
    [Route("api/auth")]
    
    [ApiController]
    [EnableCors]
    [Produces("application/json", "application/xml")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ILoggerService loggerService;
        private readonly IConfiguration configuration;
        private readonly IJwtAuthManager jwtAuthManager;
        public AuthController(IUserRepository userRepository, ILoggerService loggerService, IJwtAuthManager jwtAuthManager, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.loggerService = loggerService;
            this.jwtAuthManager = jwtAuthManager;
            this.configuration = configuration;
        }
        [AllowAnonymous]
     
        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthCreds authCreds)
        {
            User user = userRepository.GetUserByUserName(authCreds.UserName);

            if (user == null)
            {
                loggerService.Log(LogLevel.Warning, "Authenticate", "User not found");
                return NotFound();
            }
            else if (!BCrypt.Net.BCrypt.Verify(authCreds.Password, user.Password))
            {
                loggerService.Log(LogLevel.Warning, "Authenticate", "Invalid password");
                return Unauthorized();
            }
            Console.WriteLine(user.Role.ToString());
            var token = jwtAuthManager.Authenticate(authCreds.UserName, authCreds.Password, user.Role.ToString(), user.UserId);
            return Ok(new { Token = token.Token, ExpiresOn = token.ExpiresOn , Username = token.Username, Role = token.Role, UserId = token.UserId});
        }
       

    }
}
