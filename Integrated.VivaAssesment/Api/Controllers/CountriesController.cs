using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController(IMediator mediator) : ControllerBase {
        private readonly IMediator _mediator = mediator;

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAllCountries(CancellationToken cancellationToken) {
            var result = await _mediator.Send(new GetCountriesQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
