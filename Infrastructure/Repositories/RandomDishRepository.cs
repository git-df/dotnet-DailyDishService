using Domain.Interfaces;
using Domain.Models;
using Domain.Options;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RandomDishRepository : IRandomDishRepository
    {
        private readonly RandomDishOptions _randomDishOptions;
        private const double MinValue = 20;
        private const double MaxValue = 100;

        public RandomDishRepository(IOptions<RandomDishOptions> randomDishOptions)
        {
            _randomDishOptions = randomDishOptions.Value;
        }

        public async Task<RandomDishDto> GetDishAsync(CancellationToken cancellationToken = default)
        {
            var url = _randomDishOptions.RandomDishUrl;

            var response = await url.GetJsonAsync<MealResponse>(cancellationToken: cancellationToken);

            var meal = response.Meals.FirstOrDefault();

            return new RandomDishDto(meal.StrMeal, GetRandomPrice());
        }

        static double GetRandomPrice()
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * (MaxValue - MinValue) + MinValue, 2);
        }
    }
}
