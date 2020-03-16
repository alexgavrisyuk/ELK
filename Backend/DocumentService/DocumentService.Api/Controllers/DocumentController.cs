using System.Threading.Tasks;
using DocumentService.Supervisor.Commands.DocumentCommands;
using DocumentService.Supervisor.Queries.DocumentQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Api.Controllers
{
    [Route("api/Documents")]
    public class DocumentController : BaseController
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var query = new GetAllDocumentsQuery();
            
            var response = await _mediator.Send(query);
            return JsonResult(response);
        }
        
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetAsync([FromRoute] string name)
        {
            var query = new GetDocumentByNameQuery()
            {
                Name = name
            };
            
            var response = await _mediator.Send(query);
            return JsonResult(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDocumentCommand command)
        {   
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDocumentCommand command)
        {   
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
        
        [HttpDelete]
        [Route("{name}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {   
            var command = new DeleteDocumentCommand
            {
                Id = id
            };
            
            var response = await _mediator.Send(command);
            return JsonResult(response);
        }
    }
}