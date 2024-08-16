using AutoFixture;
using Domain.Interfaces;
using Moq;

namespace Application.Tests
{
    public class BaseTests
    {
        protected Fixture _fixture;
        protected Mock<ICacheRepository> _cacheRepository;

        public BaseTests()
        {
            _fixture = new Fixture();
            _cacheRepository = new Mock<ICacheRepository>();
        }

        protected void MockGetAsync<T>(T value)
        {
            _cacheRepository
                .Setup(x => x.GetAsync<T>(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(value);
        }
    }
}
