using NodaTime;

using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Domain.Models.Test.Generic;

public abstract class GenericToHistEntityTest<TEntity, THistEntity, TId>
    where TEntity : Entity<THistEntity, TId>
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    private readonly IClock _clock;
    private readonly IEnumerable<TEntity> _testEntities;
    private readonly TimeSpan _testPrecision = TimeSpan.FromMilliseconds(10);

    protected GenericToHistEntityTest(IClock clock, params TEntity[] testEntities)
    {
        _clock = clock;
        _testEntities = testEntities;
    }

    [Fact]
    public void ShouldHistEntityHaveAllEntityProperties()
    {
        var entityProperties = typeof(TEntity)
            .GetProperties()
            .Select(x => (x.Name, x.PropertyType))
            .ToList();
        var histEntityProperties = typeof(THistEntity)
            .GetProperties()
            .Select(x => (x.Name, x.PropertyType))
            .ToList();
        foreach (var entityProperty in entityProperties)
        {
            var present = histEntityProperties.Contains(entityProperty);
            Assert.True(present, $"{entityProperty.Name} is not present in {typeof(THistEntity).Name}");
        }
    }

    [Fact]
    public void ShouldCorrectlyConvertEntityToHistEntity()
    {
        foreach (var entity in _testEntities)
        {
            var histGuidEntity = entity.ToHistEntity(_clock);

            var entityProperties = entity.GetType().GetProperties();
            var histEntityProperties = histGuidEntity.GetType().GetProperties();

            foreach (var entityProperty in entityProperties)
            {
                var expected = entityProperty.GetValue(entity);
                var actual = histEntityProperties.FirstOrDefault(x => x.Name == entityProperty.Name)
                    ?.GetValue(histGuidEntity);

                Assert.Equal(expected, actual);
            }
        }
    }

    [Fact]
    public void ShouldCorrectlySetHistEntityHistFrom()
    {
        foreach (var entity in _testEntities)
        {
            var histGuidEntity = entity.ToHistEntity(_clock);

            var expected = _clock.GetCurrentInstant();
            var actual = histGuidEntity.HistFrom;

            Assert.Equal(expected.ToDateTimeUtc(), actual.ToDateTimeUtc(), _testPrecision);
        }
    }
}
