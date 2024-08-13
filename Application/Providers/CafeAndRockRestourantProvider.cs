using Domain.Consts;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Providers
{
    public class CafeAndRockRestourantProvider : IRestourantProvider
    {
        private readonly IRandomDishRepository _randomDishRepository;
        private const string CafeAndRockDishFormat = "{0}: {1} - {2} $";
        private static readonly int[] sourceArray = [1, 2, 3];

        public string Name => RestourantNames.CafeAndRock;


        public CafeAndRockRestourantProvider(IRandomDishRepository randomDishRepository)
        {
            _randomDishRepository = randomDishRepository;
        }

        public async Task<object> GetValueAsync(CancellationToken cancellationToken = default)
        {
            var tasks = (sourceArray)
                .Select(async x =>
                {
                    var dish = await _randomDishRepository.GetDishAsync(cancellationToken);
                    return string.Format(CafeAndRockDishFormat, x, dish.Name, dish.Price);
                });

            var menu = await Task.WhenAll(tasks);

            return new CafeAndRockDto(menu?.ToList());
        }
    }
}
