using Domain.Consts;
using Domain.Interfaces;
using MediatR;

namespace Application.DailyDish.Queries.Exists
{
    public class ExistsQueryHandler : IRequestHandler<ExistsQuery, IEnumerable<string>>
    {
        private readonly ICacheRepository _cacheRepository;

        public ExistsQueryHandler(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<IEnumerable<string>> Handle(ExistsQuery request, CancellationToken cancellationToken)
        {
            var existsNames = _cacheRepository.Get<IEnumerable<string>>(CacheKeys.ExistsDataRestourant);

            return existsNames?
                .Where(x => request.Names is null || request.Names.Contains(x)) ?? [];
        }
    }
}
