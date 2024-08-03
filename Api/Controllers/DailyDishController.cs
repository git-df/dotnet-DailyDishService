using Application.DailyDish.Queries.Exists;
using Application.DailyDish.Queries.List;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyDishController : ControllerBase
    {
        private readonly ISender _sender;

        public DailyDishController(IMediator mediator)
        {
            _sender = mediator;
        }

        [HttpGet("list")]
        public async Task<Dictionary<string, object>> ListAsync([FromQuery]ListQuery query, CancellationToken cancellationToken = default)
            => await _sender.Send(query, cancellationToken);

        [HttpGet("exists")]
        public async Task<IEnumerable<ExistsDto>> ExistsAsync([FromQuery]ExistsQuery query, CancellationToken cancellationToken = default)
            => await _sender.Send(query, cancellationToken);
    }
}
