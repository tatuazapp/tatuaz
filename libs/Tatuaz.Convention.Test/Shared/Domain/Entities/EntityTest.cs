using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Xunit;

namespace Tatuaz.Convention.Test.Shared.Domain.Entities;

public class EntityTest
{
    private readonly List<Type> _entityTypes;
    private readonly List<Type> _histEntityTypes;

    public EntityTest()
    {
        _entityTypes = typeof(IHistDumpableEntity).Assembly
            .GetTypes()
            .Where(x => typeof(IHistDumpableEntity).IsAssignableFrom(x) && typeof(IHistDumpableEntity) != x)
            .ToList();
        _histEntityTypes = typeof(HistEntity).Assembly
            .GetTypes()
            .Where(x => typeof(HistEntity).IsAssignableFrom(x) && typeof(HistEntity) != x)
            .ToList();
    }

    public class HistIntegration : EntityTest
    {
        [Fact]
        public void Should_HistCounterpartsExistWithCorrectName()
        {
            foreach (var entityType in _entityTypes)
            {
                var toCut = entityType.Name.IndexOf('`');
                toCut = toCut == -1 ? entityType.Name.Length : toCut;
                var value = $"Hist{entityType.Name.Substring(0, toCut)}";

                var present = _histEntityTypes.Any(
                    x =>
                    {
                        var toCut2 = x.Name.IndexOf('`');
                        toCut2 = toCut2 == -1 ? x.Name.Length : toCut2;
                        return x.Name.Substring(0, toCut2) == value;
                    });
                Assert.True(
                    present,
                    $"{entityType.Name} doesn't have a Hist counterpart named {value}"
                );
            }
        }
    }

    public class TypeConfiguration : EntityTest
    {
        [Fact]
        public void Should_EntityConfigurationExist()
        {
            var validEntities = _entityTypes.Where(x =>
                x.GetCustomAttribute(typeof(BaseEntityAttribute)) == null).ToList();
            Assert.All(validEntities, entityType =>
            {
                // check if IEntityTypeConfiguration exists for this entityType
                var entityTypeConfiguration = typeof(IEntityTypeConfiguration<>).MakeGenericType(entityType);
                var entityTypeConfigurationExists = typeof(Entity<,>).Assembly
                    .GetTypes()
                    .Any(x => x.IsAssignableTo(entityTypeConfiguration));

                Assert.True(
                    entityTypeConfigurationExists,
                    $"IEntityTypeConfiguration<{entityType.Name}> doesn't exist"
                );
            });
        }

        [Fact]
        public void Should_HistEntityConfigurationExist()
        {
            var validHistEntities = _histEntityTypes.Where(x =>
                x.GetCustomAttribute(typeof(BaseHistEntityAttribute)) == null).ToList();
            Assert.All(validHistEntities, histEntityType =>
            {
                // check if IEntityTypeConfiguration exists for this entityType
                var entityTypeConfiguration = typeof(IEntityTypeConfiguration<>).MakeGenericType(histEntityType);
                var entityTypeConfigurationExists = typeof(HistEntity<>).Assembly
                    .GetTypes()
                    .Any(x => x.IsAssignableTo(entityTypeConfiguration));

                Assert.True(
                    entityTypeConfigurationExists,
                    $"IEntityTypeConfiguration<{histEntityType.Name}> doesn't exist"
                );
            });
        }
    }
}
