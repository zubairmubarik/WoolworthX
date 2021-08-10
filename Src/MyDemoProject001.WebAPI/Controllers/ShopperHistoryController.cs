using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDemoProject001.Application.ShopperHistory.Commands.CreateShopperHistory;
using MyDemoProject001.Application.ShopperHistory.Queries.GetShopperHistoryListAsProductDto;
using MyDemoProject001.Application.ShopperHistory.Queries.GetShopperList;
using MyDemoProject001.Domain.Entities;

using System;
using System.Threading.Tasks;

namespace MyDemoProject001.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopperHistoryController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ShopperHistoryController(IMediator mediator, ILogger<ShopperHistoryController> logger)
        {
            _mediator = mediator;
            _logger = logger;
            _logger.LogInformation($"Executing Controller:{this.GetType().Name}");
        }

        [HttpGet("GetShopperHistory", Name = "GetShopperHistory")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _mediator.Send(new GetShopperHistoryListQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("ShopperHistoryAsProductDto", Name = "ShopperHistoryAsProductDto")]
        public async Task<IActionResult> GetAsProductDto()
        {
            try
            {
                return Ok(await _mediator.Send(new GetShopperHistoryListAsProductDtoQuery()));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost("AddShopperHistory/", Name = "AddShopperHistory")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(ShopperHistoryDocument shopperHistory)
        {
            _logger.LogInformation("Creating New Shopper History");

            var result = await _mediator.Send(new CreateShopperHistoryCommand(shopperHistory));

            if (result != null)
            {
                _logger.LogInformation("New record saved JSON:{0}", result.CustomerId);
            }

            return Ok(result);
        }
    }
}
