using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus;
using NodaTime;
using NodaTime.Testing;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Xunit;

namespace Tatuaz.Shared.Domain.Entities.Test.Models;

public class ToHistGenericTest
{
    // [Fact]
    // public void Should_CorrectlyConvertToHistEntity_ForAllEntities()
    // {
    //     var entityTypes = EntityHelpers
    //         .GetTestableEntityTypes()
    //         .Where(x => x.IsAssignableTo(typeof(IHistDumpableEntity)));
    //     var fakeClock = new FakeClock(Instant.FromUtc(2021, 1, 1, 0, 0));
    //
    //     Assert.All(
    //         entityTypes,
    //         entityType =>
    //         {
    //             var entity = GetPopulatedEntity(entityType);
    //             var addedHistEntity = ((IHistDumpableEntity)entity).ToHistEntity(
    //                 fakeClock,
    //                 HistState.Added
    //             );
    //             var modifiedHistEntity = ((IHistDumpableEntity)entity).ToHistEntity(
    //                 fakeClock,
    //                 HistState.Modified
    //             );
    //             var deletedHistEntity = ((IHistDumpableEntity)entity).ToHistEntity(
    //                 fakeClock,
    //                 HistState.Deleted
    //             );
    //
    //             Assert.Equal(HistState.Added, addedHistEntity.HistState);
    //             Assert.Equal(HistState.Modified, modifiedHistEntity.HistState);
    //             Assert.Equal(HistState.Deleted, deletedHistEntity.HistState);
    //             Assert.Equal(
    //                 fakeClock.GetCurrentInstant().ToDateTimeUtc(),
    //                 addedHistEntity.HistDumpedAt.ToDateTimeUtc(),
    //                 TimeSpan.FromMilliseconds(10)
    //             );
    //             Assert.Equal(
    //                 fakeClock.GetCurrentInstant().ToDateTimeUtc(),
    //                 modifiedHistEntity.HistDumpedAt.ToDateTimeUtc(),
    //                 TimeSpan.FromMilliseconds(10)
    //             );
    //             Assert.Equal(
    //                 fakeClock.GetCurrentInstant().ToDateTimeUtc(),
    //                 deletedHistEntity.HistDumpedAt.ToDateTimeUtc(),
    //                 TimeSpan.FromMilliseconds(10)
    //             );
    //             Assert.Empty(
    //                 EntityContainsHistPropertiesAndTheyMatch(
    //                     (IHistDumpableEntity)entity,
    //                     addedHistEntity
    //                 )
    //             );
    //             Assert.Empty(
    //                 EntityContainsHistPropertiesAndTheyMatch(
    //                     (IHistDumpableEntity)entity,
    //                     modifiedHistEntity
    //                 )
    //             );
    //             Assert.Empty(
    //                 EntityContainsHistPropertiesAndTheyMatch(
    //                     (IHistDumpableEntity)entity,
    //                     deletedHistEntity
    //                 )
    //             );
    //         }
    //     );
    // }

    private static object GetPopulatedEntity(Type type)
    {
        var genericFakerType = typeof(Faker<>).MakeGenericType(type);
        var concreteFakerType = typeof(GuidEntityFaker).Assembly
            .GetTypes()
            .FirstOrDefault(x => x.IsSubclassOf(genericFakerType));
        var faker = Activator.CreateInstance(
            concreteFakerType ?? throw new InvalidOperationException()
        );

        return ((dynamic)faker!).Generate();
    }

    private static IEnumerable<PropertyInfo> EntityContainsHistPropertiesAndTheyMatch(
        IHistDumpableEntity entity,
        HistEntity histEntity
    )
    {
        var histEntityProperties = histEntity
            .GetType()
            .GetProperties()
            .Where(
                x =>
                    x.Name != nameof(HistEntity.HistState)
                    && x.Name != nameof(HistEntity.HistDumpedAt)
                    && x.Name != nameof(HistEntity.HistId)
            )
            .ToList();
        var entityProperties = entity.GetType().GetProperties().ToList();
        return histEntityProperties.Where(
            histEntityProperty =>
                !PropertiesMatch(histEntityProperty, entity, histEntity, entityProperties)
        );
    }

    private static bool PropertiesMatch(
        PropertyInfo histEntityProperty,
        IHistDumpableEntity entity,
        HistEntity histEntity,
        IEnumerable<PropertyInfo> entityProperties
    )
    {
        // accept enum counterparts compared by int value
        if (histEntityProperty.PropertyType.IsEnum)
        {
            return histEntityProperty.GetValue(histEntity) as int?
                == entityProperties
                    .FirstOrDefault(
                        x =>
                            x.Name == histEntityProperty.Name
                            || "Hist" + x.Name == histEntityProperty.Name
                    )
                    ?.GetValue(entity) as int?;
        }

        return histEntityProperty
                .GetValue(histEntity)
                ?.Equals(
                    entityProperties
                        .FirstOrDefault(
                            x =>
                                x.Name == histEntityProperty.Name
                                || "Hist" + x.Name == histEntityProperty.Name
                        )
                        ?.GetValue(entity)
                ) == true;
    }
}
