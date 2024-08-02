using Domain.Parameters;
using MediatR;

namespace Application.DailyDish.Queries.List
{
    public class ListQuery : DailyDishBaseParameters, IRequest<Dictionary<string, object>>
    {
    }
}
