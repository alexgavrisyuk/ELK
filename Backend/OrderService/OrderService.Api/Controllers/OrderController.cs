using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Api.Helpers;
using OrderService.Ordering.Commands;
using OrderService.Ordering.Commands.OrderCommands;
using OrderService.Ordering.Queries;
using OrderService.Ordering.Queries.OrderQueries;

namespace OrderService.Api.Controllers
{
    [Authorize(AuthenticationSchemes = Constants.AuthScheme.Auth)]
    [Route("api/Orders")]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetOrdersQuery();
            
            var response = await _mediator.Send(query);
            return JsonResult(response);
        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] long id)
        {
            var query = new GetOrderByIdQuery
            {
                Id = id
            };
            
            var response = await _mediator.Send(query);
            return JsonResult(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderCommand command)
        {   
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrderCommand command)
        {   
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {   
            var command = new DeleteOrderCommand
            {
                Id = id
            };
            
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
    }
}