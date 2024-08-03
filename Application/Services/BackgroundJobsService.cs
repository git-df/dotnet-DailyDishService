using Domain.Consts;
using Domain.Interfaces;
using Domain.Models;
using System.Threading;

namespace Application.Services
{
    public class BackgroundJobsService : IBackgroundJobsService
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IEnumerable<IRestourantProvider> _restourantProviders;

        public BackgroundJobsService(ICacheRepository cacheRepository, IEnumerable<IRestourantProvider> restourantProviders)
        {
            _cacheRepository = cacheRepository;
            _restourantProviders = restourantProviders;
        }

        public async Task ProcessDailyDish(CancellationToken cancellationToken = default)
        {
            var now = DateTime.Now;
            var allNames = _restourantProviders.Select(x => x.Name);
            var exists = _cacheRepository.Get<IEnumerable<ExistsDto>>(CacheKeys.ExistsDataRestourant) ?? [];
            exists = exists.Where(x => x.CreatedDate.Date == now.Date);

            if (exists.Count() == allNames.Count()) 
            {
                return;
            }


            var names = exists.Select(x => x.Name);
            var tasks = _restourantProviders
                .Where(x => !names.Contains(x.Name))
                .Select(async x =>
                {
                    return new
                    {
                        x.Name,
                        Value = await x.GetValueAsync(cancellationToken)
                    };
                });

            var results = await Task.WhenAll(tasks);

            var newNames = results
                .Where(x => x.Value is not null)
                .Select(x =>
                {
                    _cacheRepository.Set(x.Name, x.Value);
                    return new ExistsDto(
                        CreatedDate: now,
                        Name: x.Name);
                });

            _cacheRepository.Set(CacheKeys.ExistsDataRestourant, exists.Concat(newNames));
        }
    }
}
