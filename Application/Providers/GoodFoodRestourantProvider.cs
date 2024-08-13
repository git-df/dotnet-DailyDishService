using Domain.Consts;
using Domain.Interfaces;
using Domain.Models;
using System.Net.Http.Headers;

namespace Application.Providers
{
    public class GoodFoodRestourantProvider : IRestourantProvider
    {
        private readonly IRandomDishRepository _randomDishRepository;
        private static readonly int[] sourceArray = [1, 2, 3, 4, 5];

        public string Name => RestourantNames.GoodFood;

        public GoodFoodRestourantProvider(IRandomDishRepository randomDishRepository)
        {
            _randomDishRepository = randomDishRepository;
        }

        public async Task<object> GetValueAsync(CancellationToken cancellationToken = default)
        {
            var tasks = (sourceArray)
                .Select(async x =>
                {
                    var dish = await _randomDishRepository.GetDishAsync(cancellationToken);
                    return new GoodFoodDto(dish.Name, dish.Price);
                });

            var menu = await Task.WhenAll(tasks);

            return menu;
        }
    }
}
