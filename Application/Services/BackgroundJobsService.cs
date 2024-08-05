using Domain.Consts;
using Domain.Interfaces;
using Domain.Models;

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
            var exists = await _cacheRepository.GetAsync<IEnumerable<ExistsDto>>(CacheKeys.ExistsDataRestourant, cancellationToken) ?? [];
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

            var newNamesTasks = results
                .Where(x => x.Value is not null)
                .Select(async x =>
                {
                    await _cacheRepository.SetAsync(x.Name, x.Value, cancellationToken);
                    return new ExistsDto(
                        CreatedDate: now,
                        Name: x.Name);
                });

            var newNames = await Task.WhenAll(newNamesTasks);


            await _cacheRepository.SetAsync(CacheKeys.ExistsDataRestourant, exists.Union(newNames), cancellationToken);
        }
    }
}
