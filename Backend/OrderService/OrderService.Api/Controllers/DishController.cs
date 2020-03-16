using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Helpers;
using OrderService.Ordering.Commands.DishCommands;
using OrderService.Ordering.Queries.DishQueries;

namespace OrderService.Api.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.AuthScheme.Auth)]
    [Route("api/Dishes")]
    public class DishController : BaseController
    {
        private readonly IMediator _mediator;

        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetDishesQuery();
            
            var response = await _mediator.Send(query);
            return JsonResult(response);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
        {
            var query = new GetDishByIdQuery
            {
                Id = id
            };
            
            var response = await _mediator.Send(query);
            return JsonResult(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDishCommand command)
        {   
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDishCommand command)
        {   
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {   
            var command = new DeleteDishCommand
            {
                Id = id
            };
            
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
    }
}