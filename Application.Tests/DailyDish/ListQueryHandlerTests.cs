using Application.DailyDish.Queries.Exists;
using Application.DailyDish.Queries.List;
using AutoFixture;
using Domain.Models;
using Moq;

namespace Application.Tests.DailyDish
{
    public class ListQueryHandlerTests : BaseTests
    {
        private ListQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new ListQueryHandler(_cacheRepository.Object);
        }

        [Test]
        public async Task ReturnsCorrectResponse_WithoutFilter()
        {
            var now = DateTime.Now;
            var query = _fixture
                .Build<ListQuery>()
                .Without(x => x.Names)
                .Create();

            var existsNames = _fixture
                .Build<ExistsDto>()
                .With(x => x.CreatedDate, now)
                .CreateMany();

            MockGetAsync(existsNames);

            var result = await _handler
                .Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public async Task ReturnsCorrectResponse_WithFilter()
        {
            var now = DateTime.Now;
            var names = _fixture.CreateMany<string>(2);
            var query = _fixture
                .Build<ListQuery>()
                .With(x => x.Names, names.ToList)
                .Create();

            var existsNames = _fixture
                .Build<ExistsDto>()
                .With(x => x.CreatedDate, now)
                .CreateMany();
            var namesFromPatameters = names
                .Select(x => new ExistsDto(now, x));

            MockGetAsync(existsNames.Union(namesFromPatameters));

            var result = await _handler
                .Handle(query, CancellationToken.None);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }
    }
}
