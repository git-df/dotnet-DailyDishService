using Domain.Consts;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using static Application.Providers.CafeAndRockRestourantProvider;
using static Application.Providers.GoodFoodRestourantProvider;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICacheRepository _cacheRepository;
        private readonly IEnumerable<IRestourantProvider> _restourantProviders;

        public TestController(ICacheRepository cacheRepository, IEnumerable<IRestourantProvider> restourantProviders)
        {
            _cacheRepository = cacheRepository;
            _restourantProviders = restourantProviders;
        }

        [HttpPut]
        public async Task WriteData(CancellationToken cancellationToken = default)
        {
            var tasks = _restourantProviders.Select(async x =>
            {
                return new
                {
                    x.Name,
                    Value = await x.GetValueAsync(cancellationToken)
                };
            });

            var results = await Task.WhenAll(tasks);

            var exists = results
                .Where(x => x.Value is not null)
                .Select(x =>
                {
                    _cacheRepository.Set(x.Name, x.Value);
                    return x.Name;
                });

            _cacheRepository.Set(CacheKeys.ExistsDataRestourant, exists);
        }
    }
}
