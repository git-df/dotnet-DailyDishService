using Domain.Consts;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Application.DailyDish.Queries.Exists
{
    public class ExistsQueryHandler : IRequestHandler<ExistsQuery, IEnumerable<ExistsDto>>
    {
        private readonly ICacheRepository _cacheRepository;

        public ExistsQueryHandler(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<IEnumerable<ExistsDto>> Handle(ExistsQuery request, CancellationToken cancellationToken)
        {
            var existsNames = _cacheRepository.Get<IEnumerable<ExistsDto>>(CacheKeys.ExistsDataRestourant);

            return existsNames?
                .Where(x => request.Names is null || request.Names.Contains(x.Name))
                .Where(x => x.CreatedDate.Date == DateTime.Now.Date) ?? [];
        }
    }
}
