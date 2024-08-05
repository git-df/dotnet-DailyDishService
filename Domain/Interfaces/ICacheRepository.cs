
namespace Domain.Interfaces
{
    public interface ICacheRepository
    {
        Task<T> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default);
        Task SetAsync(string cacheKey, object value, CancellationToken cancellationToken = default);
    }
}
