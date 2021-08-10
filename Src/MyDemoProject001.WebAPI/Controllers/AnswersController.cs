using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyDemoProject001.Application.Common.Extensions;
using MyDemoProject001.Application.Common.Interfaces;
using MyDemoProject001.Application.Common.Models;
using MyDemoProject001.Application.Products.Queries.GetProductSortList;
using MyDemoProject001.Application.Users.Queries.GetUser;
using System.Threading.Tasks;

namespace MyDemoProject001.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswersController : ControllerBase
    {
        private readonly ILogger<AnswersController> _logger;
        private readonly IMediator _mediator;
        private readonly ITrolleyCalculator _trolleyCalculator;

        public AnswersController(IMediator mediator, ILogger<AnswersController> logger, ITrolleyCalculator trolleyCalculator)
        {
            _mediator = mediator;
            _logger = logger;
            _trolleyCalculator = trolleyCalculator;
            _logger.LogInformation($"Executing Controller:{this.GetType().Name}");
        }

        [HttpGet("User")]       
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            //TODO : Handle exception through handler

            var result = await _mediator.Send(new GetUserQuery());

            if (result != null)
                return Ok(result);
            else
                return NotFound();

        }

        [HttpGet("Sort")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(string sortOption)
        {
            //Exception Handleds in Middleware Hnadler
            if (sortOption.IsStringEmpty())
            {
                return BadRequest("Invalid request");
            }

            return Ok(await _mediator.Send(new GetProductSortListQuery(sortOption)));

        }


        [HttpPost("TrolleyTotal")]
        [AllowAnonymous]
        public IActionResult TrolleyTotal(ShoppingListDto shoppingList)
        {
            //Exception Handleds in Middleware Hnadler

            return Ok(_trolleyCalculator.CalculatorAsync(shoppingList));
        }
    }
}
