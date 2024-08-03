

namespace Domain.Interfaces
{
    public interface IBackgroundJobsService
    {
        Task ProcessDailyDish(CancellationToken cancellationToken = default);
    }
}
