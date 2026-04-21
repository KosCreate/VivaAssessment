using Application.Commands;
using Application.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ComputeIntegerCollectionController(IMediator mediator) : ControllerBase {
        private readonly IMediator _mediator = mediator;

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> GetSecondLargestNumber([FromBody] RequestObj requestObj, CancellationToken cancellationToken) {
            var request = await _mediator.Send(new GetSecondLargestNumberCommand(requestObj), cancellationToken);
            return Ok(request);
        }
    }
}
