using AutoMapper;
using Backlog_MicroService.Data;
using Backlog_MicroService.Entities;
using Backlog_MicroService.Models.Backlog;
using Backlog_MicroService.Models.ProductBacklog;
using Backlog_MicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backlog_MicroService.Controllers
{
    [ApiController]
    [Route("api/backlogs/productBacklogs")]
    [Produces("application/json", "application/xml")]
    public class ProductBacklogController :Controller
    {
        private readonly IMapper mapper;
        private readonly IProductBacklogRepository productBacklogRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly ILoggerService loggerService;

        public ProductBacklogController (IMapper mapper, IProductBacklogRepository productBacklogRepository, LinkGenerator linkGenerator, ILoggerService loggerService)
        {
            this.mapper = mapper;
            this.productBacklogRepository = productBacklogRepository;
            this.linkGenerator = linkGenerator;
            this.loggerService = loggerService;
        }

        [HttpGet]
        [HttpHead] //Podržavamo i HTTP head zahtev koji nam vraća samo zaglavlja u odgovoru    
        [ProducesResponseType(StatusCodes.Status200OK)] //Eksplicitno definišemo šta sve može ova akcija da vrati
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Admin, ProductOwner")]
        public async Task<ActionResult<List<ProductBacklog>>> GetProductBacklogs()
        {
            List<ProductBacklog> productBacklogs = productBacklogRepository.GetProductBacklogs();
            if (productBacklogs == null || productBacklogs.Count == 0)
            {
                await loggerService.Log(LogLevel.Warning, "GetProductBacklogs", "Product backlog not found.");
                NoContent();
            }
            Console.WriteLine(await loggerService.Log(LogLevel.Information, "GetProductBacklogs", "Product backlog successfully returned."));
            return Ok(mapper.Map<List<ProductBacklogDto>>(productBacklogs));
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster, Stakeholder, Developer")]
        [HttpGet("{productBacklogId}")]
        public async Task<ActionResult<ProductBacklog>> GetProductBacklog(Guid productBacklogId)
        {
            ProductBacklog productBacklogModel = productBacklogRepository.GetProductBacklogId(productBacklogId);
            if (productBacklogModel == null)
            {
                await loggerService.Log(LogLevel.Warning, "GetProductBacklog", "Product backlog with id: " + productBacklogId + " not found.");
                return NotFound();
            }

            await loggerService.Log(LogLevel.Information, "GetProductBacklog", "Product backlog with id: " + productBacklogId + " successfully returned.");
            return Ok(mapper.Map<ProductBacklogDto>(productBacklogModel));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner, ScrumMaster")]
        public async Task<ActionResult<ProductBacklogConfirmation>> CreateProductBacklog([FromBody] ProductBacklogCreationDto productBacklog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Vraćajte 400 Bad Request za validacione greške
                }


                ProductBacklog mappedProductBacklog = mapper.Map<ProductBacklog>(productBacklog);
                mappedProductBacklog.IdBacklog = Guid.NewGuid();
                ProductBacklogConfirmation confirmationProductBacklog = productBacklogRepository.AddProductBacklog(mappedProductBacklog);
                productBacklogRepository.SaveChanges();

               // string location = linkGenerator.GetPathByAction("GetProductBacklog", "ProductBacklog", new { backlogId = confirmationProductBacklog.IdBacklog });

                await loggerService.Log(LogLevel.Information, "CreateProductBacklog", "Product backlog successfully created.");
                return Ok( mapper.Map<ProductBacklogConfirmationDto>(confirmationProductBacklog));
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "CreateProductBacklog", "Error creating product backlog. ");
                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner")]
        [HttpDelete("{productBacklogId}")]
        public async Task<IActionResult> DeleteProductBacklog(Guid productBacklogId)
        {
            try
            {
                ProductBacklog productBacklog = productBacklogRepository.GetProductBacklogId(productBacklogId);
                if (productBacklog == null)
                {
                    await loggerService.Log(LogLevel.Warning, "DeleteProductBacklog", "Product backlog with id " + productBacklogId + " not found.");
                    return NotFound();
                }

                productBacklogRepository.RemoveProductBacklog(productBacklogId);
                productBacklogRepository.SaveChanges();
                return StatusCode(StatusCodes.Status204NoContent, "Uspesno obrisan product backlog!");
            }
            catch
            {
                await loggerService.Log(LogLevel.Error, "DeleteProductBacklog", "Error deleting product backlog with id " + productBacklogId + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }


        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin, ProductOwner")]
        public async Task<ActionResult<ProductBacklogDto>> UpdateProductBacklog(ProductBacklogUpdateDto productBacklog)
        {
            try
            {
                // Map the DTO to the domain model
                ProductBacklog mappedProductBacklog = mapper.Map<ProductBacklog>(productBacklog);

                // Call the repository method to update the backlog
                var updatedProductBacklog = productBacklogRepository.UpdateProductBacklog(mappedProductBacklog);

                // Map the updated epic to DTO
                ProductBacklogDto updatedProductBacklogDto = mapper.Map<ProductBacklogDto>(updatedProductBacklog);

                await loggerService.Log(LogLevel.Information, "UpdateProductBacklog", "Product backlog with id " + productBacklog + " successfully updated.");
                // Return the updated resource
                return Ok(updatedProductBacklog);
            }
            catch (KeyNotFoundException)
            {
                await loggerService.Log(LogLevel.Warning, "UpdateProductBacklog", "Product backlog with id " + productBacklog + " not found.");
                return NotFound();
            }
            catch (Exception)
            {
                await loggerService.Log(LogLevel.Error, "UpdateProductBacklog", "Error updating product backlog with id " + productBacklog + ".");
                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }

        [HttpOptions]
        [AllowAnonymous] //Dozvoljavamo pristup anonimnim korisnicima
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }


    }
}
