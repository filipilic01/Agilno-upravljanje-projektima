using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using User_Service.Data;
using User_Service.Entities;
using User_Service.Models;
using User_Service.Models.OtherModelServices;
using User_Service.Models.User;
using User_Service.Services;

namespace User_Service.Controllers
{
    
    [ApiController]
 
    [EnableCors]
    [Route("api/user")]
    [Produces("application/json", "application/xml")]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;
        private readonly IServiceCall<FAQSectionDTO> faqSectionService;
        
        private readonly IConfiguration configuration;


        public UserController(IMapper mapper, IUserRepository userRepository, LinkGenerator linkGenerator, ILoggerService loggerService,
             IServiceCall<FAQSectionDTO> faqSectionService,
             IConfiguration configuration
         )
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
            this.faqSectionService = faqSectionService;
            this.configuration = configuration;
        }

        /// <summary>
        ///     Vraća sve korisnike
        /// </summary>
        [HttpGet]
        [HttpHead]
       // [AllowAnonymous]
        [Authorize(Roles = "Admin, ScrumMaster, ProductOwner, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserDto>> GetUsers()
        {

            List<User> users = userRepository.GetUsers();

            if (users == null || users.Count == 0)
            {
                loggerService.Log(LogLevel.Warning, "GetUsers", "Users not found.");
                NoContent();
            }
            Console.WriteLine(loggerService.Log(LogLevel.Information, "GetUsers", "Users successfully returned."));
            List<UserDto> usersDto = new List<UserDto>();
            foreach(var user in users)
            {
               usersDto.Add(mapper.Map<UserDto>(user));
            }
            return Ok(usersDto);

        }
        /// <summary>
        ///     Vraća jednog korisnika po ID
        /// </summary>

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
       [Authorize(Roles = "Admin, ScrumMaster, ProductOwner, Stakeholder, Developer")]
        [HttpGet("{userId}")]
        public ActionResult<User> GetUserById(Guid userId)
        {
            User user = userRepository.GetUserById(userId);
            if (user == null)
            {
                loggerService.Log(LogLevel.Warning, "GetUserById", "User with id: " + userId + " not found.");
                return NotFound();
            }
            loggerService.Log(LogLevel.Information, "GetUserById", "User with id: " + userId + " successfully returned.");
            return Ok(mapper.Map<User>(user));
        }


        /// <summary>
        ///    Dodaje novog korisnika
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> CreateUser([FromBody] UserCreationDto user)
        {
            try
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                User userEntity = mapper.Map<User>(user);
                userEntity.UserId = Guid.NewGuid();
                UserConfirmation confirmation = userRepository.AddUser(userEntity);

               // string location = linkGenerator.GetPathByAction("GetUser", "User", new { UserId = confirmation.UserId });
                Console.WriteLine(mapper.Map<UserConfirmationDto>(confirmation));
                //loggerService.Log(LogLevel.Information, "CreateUser", "Users successfully created.");

                return Ok( mapper.Map<UserConfirmationDto>(confirmation));
            }
            catch (Exception ex)
            {
                loggerService.Log(LogLevel.Error, "CreateUser", "Error creating user. ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        /// <summary>
        ///     Brise korisnika
        /// </summary>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(Guid userId)
        {
            try
            {
                User user = userRepository.GetUserById(userId);
                if (user == null)
                {
                    loggerService.Log(LogLevel.Warning, "DeleteUser", "User with id " + userId + " not found.");
                    return NotFound();
                }

                userRepository.DeleteUser(userId);
                userRepository.SaveChanges();
                loggerService.Log(LogLevel.Information, "DeleteUser", "User with id " + userId + " succesfully deleted.");
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                loggerService.Log(LogLevel.Error, "DeleteUser", "Error deleting user with id " + userId + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }

        /// <summary>
        ///     Modifikuje postojeceg korisnika
        /// </summary>
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ScrumMaster, ProductOwner, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> UpdateUser(UserUpdateDto user)
        {
            try
            {
                var userEntity = userRepository.GetUserById(user.UserId);

                if (userEntity == null)
                {
                    loggerService.Log(LogLevel.Warning, "UpdateUser", "User with id " + user.UserId + " not found.");
                    return NotFound();
                }

                if (!string.IsNullOrEmpty(user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }

                userRepository.UpdateUser(mapper.Map<User>(user));
                loggerService.Log(LogLevel.Information, "UpdateUser", "User with id " + user.UserId + " successfully updated.");
                return Ok(mapper.Map<UserConfirmationDto>(user));
            }
            catch (Exception e)
            {
                loggerService.Log(LogLevel.Error, "UpdateUser", "Error updating user with id " + user.UserId + ".", e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Update error.");
            }
        }

        /// <summary>
        ///     Vraća trenutno ulogovanog korisnika
        /// </summary>
        [Authorize(Roles = "Admin, ScrumMaster, ProductOwner, Stakeholder, Developer")]
        [HttpGet("currentUser")]
        public IActionResult GetCurrentUser()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdClaim = User.FindFirst("UserId");
            var userId = userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
            var jwtToken = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
            var expiresOn = DateTime.UtcNow.AddHours(5);

            // Ovdje možete dohvatiti dodatne informacije o korisniku iz baze podataka
            // ili koristiti trenutne tvrdnje (claims) za izgradnju objekta s informacijama o korisniku.

            var currentUser = new
            {
                Token = jwtToken,
                ExpiresOn = expiresOn,
                Username = userName,
                Role = userRole,
                UserId =userId
                // Dodajte ostale informacije koje želite vratiti o korisniku
            };

            return Ok(currentUser);
        }

        /// <summary>
        ///     Vraća sve FAQ sekcije jednog korisnika
        /// </summary>
        [HttpGet("FAQSection/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ScrumMaster, ProductOwner, Stakeholder, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<FAQSectionDTO>> GetFAQSectionForUser(Guid userId)
        {
            string url = configuration["Services:FAQSection"];
            Console.WriteLine(url);
            var faqSectionDto = faqSectionService.SendGetRequestAsync(url + "/" + userId);

            if (faqSectionDto is not null)
            {
                loggerService.Log(LogLevel.Information, "GetFAQSectionForUser", "FAQ section for user with id: " + userId + " successfully returned.");
                return Ok(faqSectionDto);
            }
            else
            {
                loggerService.Log(LogLevel.Warning, "GetFAQSectionForUser", "FAQ section for user with id: " + userId + " not found");
                return NoContent();
            }
        }

    }
}
