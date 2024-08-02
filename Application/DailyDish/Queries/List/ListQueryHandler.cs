using Domain.Consts;
using Domain.Interfaces;
using MediatR;
using System.Linq;

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
            var existsNames = _cacheRepository.Get<IEnumerable<string>>(CacheKeys.ExistsDataRestourant);

            return existsNames?
                .Where(x => request.Names is null || request.Names.Contains(x))
                .ToDictionary(x => x, _cacheRepository.Get<object>) ?? [];
        }
    }
}
