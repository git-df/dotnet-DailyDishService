using Domain.Consts;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.DailyDish.Queries.List
{
    public class ListQueryHandler : IRequestHandler<ListQuery, Dictionary<string, object>>
    {
        private readonly ICacheRepository _cacheRepository;

        public ListQueryHandler(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<Dictionary<string, object>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            var existsNames = await _cacheRepository
                .GetAsync<IEnumerable<ExistsDto>>(CacheKeys.ExistsDataRestourant, cancellationToken) ?? [];

            var tasks = existsNames?
                .Where(x => request.Names is null || request.Names.Contains(x.Name))
                .Where(x => x.CreatedDate.Date == DateTime.Now.Date)
                .Select(async x =>
                {
                    var value = await _cacheRepository.GetAsync<object>(x.Name, cancellationToken);
                    return (x.Name, Value: value);
                });

            var results = await Task.WhenAll(tasks);

            return results
                .ToDictionary(x => x.Name, x => x.Value);
        }
    }
}
