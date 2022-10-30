using System;
using System.Linq;
using System.Reflection;
using Bogus;
using NodaTime;
using NodaTime.Testing;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Xunit;

namespace Tatuaz.Shared.Domain.Entities.Test.Models;

public class ToHistGenericTest
{
    [Fact]
    public void Should_CorrectlyConvertToHistEntity_ForAllEntities()
    {
        var entityTypes = typeof(IHistDumpableEntity).Assembly
            .GetTypes()
            .Where(x => typeof(IHistDumpableEntity).IsAssignableFrom(x) && typeof(IHistDumpableEntity) != x &&
                        x.GetCustomAttribute(typeof(BaseEntityAttribute)) == null)
            .ToList();
        var fakeClock = new FakeClock(Instant.FromUtc(2021, 1, 1, 0, 0));

        Assert.All(entityTypes, entityType =>
        {
            var entity = GetPopulatedEntity(entityType);
            var addedHistEntity = ((IHistDumpableEntity)entity).ToHistEntity(fakeClock, HistState.Added);
            var modifiedHistEntity = ((IHistDumpableEntity)entity).ToHistEntity(fakeClock, HistState.Modified);
            var deletedHistEntity = ((IHistDumpableEntity)entity).ToHistEntity(fakeClock, HistState.Deleted);

            Assert.Equal(HistState.Added, addedHistEntity.HistState);
            Assert.Equal(HistState.Modified, modifiedHistEntity.HistState);
            Assert.Equal(HistState.Deleted, deletedHistEntity.HistState);
            Assert.Equal(fakeClock.GetCurrentInstant().ToDateTimeUtc(), addedHistEntity.HistDumpedAt.ToDateTimeUtc(),
                TimeSpan.FromMilliseconds(10));
            Assert.Equal(fakeClock.GetCurrentInstant().ToDateTimeUtc(), modifiedHistEntity.HistDumpedAt.ToDateTimeUtc(),
                TimeSpan.FromMilliseconds(10));
            Assert.Equal(fakeClock.GetCurrentInstant().ToDateTimeUtc(), deletedHistEntity.HistDumpedAt.ToDateTimeUtc(),
                TimeSpan.FromMilliseconds(10));
            Assert.True(EntityContainsHistPropertiesAndTheyMatch((IHistDumpableEntity)entity, addedHistEntity));
            Assert.True(EntityContainsHistPropertiesAndTheyMatch((IHistDumpableEntity)entity, modifiedHistEntity));
            Assert.True(EntityContainsHistPropertiesAndTheyMatch((IHistDumpableEntity)entity, deletedHistEntity));
        });
    }

    private static object GetPopulatedEntity(Type type)
    {
        var genericFakerType = typeof(Faker<>).MakeGenericType(type);
        var concreteFakerType = typeof(GuidEntityFaker).Assembly
            .GetTypes()
            .FirstOrDefault(x => x.IsSubclassOf(genericFakerType));
        var faker = Activator.CreateInstance(concreteFakerType ?? throw new InvalidOperationException());

        return ((dynamic)faker!).Generate();
    }

    private static bool EntityContainsHistPropertiesAndTheyMatch(IHistDumpableEntity entity, HistEntity histEntity)
    {
        var histEntityProperties = histEntity.GetType().GetProperties().Where(x =>
            x.Name != nameof(HistEntity.HistState) && x.Name != nameof(HistEntity.HistDumpedAt) &&
            x.Name != nameof(HistEntity.HistId)).ToList();
        var entityProperties = entity.GetType().GetProperties().ToList();
        var match = true;
        foreach (var histEntityProperty in histEntityProperties.Where(histEntityProperty =>
                     !histEntityProperty.GetValue(histEntity)!.Equals(entityProperties
                         .FirstOrDefault(x => x.Name == histEntityProperty.Name)?.GetValue(entity))))
        {
            match = false;
        }

        return match;
    }
}
