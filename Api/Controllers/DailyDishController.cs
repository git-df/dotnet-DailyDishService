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

        /// <summary>
        /// The endpoint returns the daily dishes for individual restaurants
        /// </summary>
        /// <param name="query">Query containing a filter of restaurant names</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Dictionary of restourant daily dish data</returns>
        [HttpGet("list")]
        public async Task<Dictionary<string, object>> ListAsync([FromQuery]ListQuery query, CancellationToken cancellationToken = default)
            => await _sender.Send(query, cancellationToken);

        /// <summary>
        /// The endpoint returns the names of restaurants for which data can be retrieved
        /// </summary>
        /// <param name="query">Query containing a filter of restaurant names</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of restourant names</returns>
        [HttpGet("exists")]
        public async Task<IEnumerable<ExistsDto>> ExistsAsync([FromQuery]ExistsQuery query, CancellationToken cancellationToken = default)
            => await _sender.Send(query, cancellationToken);
    }
}
