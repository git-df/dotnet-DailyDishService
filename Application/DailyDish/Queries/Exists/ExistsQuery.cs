using Domain.Models;
using Domain.Parameters;
using MediatR;

namespace Application.DailyDish.Queries.Exists
{
    public class ExistsQuery : DailyDishBaseParameters, IRequest<IEnumerable<ExistsDto>>
    {
    }
}
