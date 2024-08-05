namespace Domain.Interfaces
{
    public interface IRestourantProvider
    {
        public string Name { get; }

        Task<object> GetValueAsync(CancellationToken cancellationToken = default);
    }
}
