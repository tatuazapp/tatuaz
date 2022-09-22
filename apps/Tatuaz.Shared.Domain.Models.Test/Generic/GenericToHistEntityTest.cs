using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Domain.Models.Test.Common;

public abstract class GenericToHistEntityTest<TEntity, THistEntity, TId>
    where TEntity : Entity<THistEntity, TId>
    where THistEntity : HistEntity<TId>, new()
    where TId : notnull
{
    private IEnumerable<TEntity> _testEntities;
    private TimeSpan _testPrecision = TimeSpan.FromMilliseconds(10);

    protected GenericToHistEntityTest(params TEntity[] testEntities)
    {
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
            var histGuidEntity = entity.ToHistEntity();

            var entityProperties = entity.GetType().GetProperties();
            var histEntityProperties = histGuidEntity.GetType().GetProperties();

            foreach (var entityProperty in entityProperties)
            {
                var expected = entityProperty.GetValue(entity);
                var actual = histEntityProperties.FirstOrDefault(x => x.Name == entityProperty.Name)?.GetValue(histGuidEntity);

                Assert.Equal(expected, actual);
            }
        }
    }
    
    [Fact]
    public void ShouldCorrectlySetHistEntityHistFrom()
    {
        foreach (var entity in _testEntities)
        {
            var histGuidEntity = entity.ToHistEntity();

            var expected = DateTime.UtcNow;
            var actual = histGuidEntity.HistFrom;

            Assert.Equal(expected, actual, _testPrecision);
        }
    }
}