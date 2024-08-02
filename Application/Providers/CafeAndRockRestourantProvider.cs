using Domain.Consts;
using Domain.Interfaces;
using Domain.Responses;

namespace Application.Providers
{
    public class CafeAndRockRestourantProvider : IRestourantProvider
    {
        public string Name => RestourantNames.CafeAndRock;

        public async Task<object> GetValueAsync(CancellationToken cancellationToken = default)
        {
            var response = new CafeAndRockDto();

            return response;
        }

        public class CafeAndRockDto : DailyDishBaseResponse
        {
            public List<string> Menu { get; set; }
        }
    }
}
