using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDemoProject001.Application.Products.Commands.CreateProductCommand;
using MyDemoProject001.Application.Products.Queries.GetProductList;
using MyDemoProject001.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace MyDemoProject001.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator, ILogger<ProductsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation($"Executing Controller:{this.GetType().Name}");
        }

        [HttpGet("Get", Name = "GetProducts")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _mediator.Send(new GetProductListQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

    

        [HttpPost("AddProduct/", Name = "AddProduct")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(ProductDocument product)
        {
            _logger.LogInformation("Creating New Product");

            var result = await _mediator.Send(new CreateProductCommand(product));

            if (result != null)
            {
                _logger.LogInformation("New record saved JSON:{0}", result.Name);
            }

            return Ok(result);
        }

    }
}
