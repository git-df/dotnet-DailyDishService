using Domain.Consts;
using Domain.Interfaces;
using Domain.Responses;

namespace Application.Providers
{
    public class GoodFoodRestourantProvider : IRestourantProvider
    {
        public string Name => RestourantNames.GoodFood;

        public async Task<object> GetValueAsync(CancellationToken cancellationToken = default)
        {
            var response = new GoodFoodDto();

            return response;
        }

        public class GoodFoodDto : DailyDishBaseResponse
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }
    }
}
