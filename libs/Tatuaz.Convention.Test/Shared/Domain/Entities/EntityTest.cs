using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tatuaz.Shared.Domain.Entities;
using Tatuaz.Shared.Domain.Entities.Fakers;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Xunit;

namespace Tatuaz.Convention.Test.Shared.Domain.Entities;

public class EntityTest
{
    private readonly List<Type> _entities;
    private readonly List<Type> _entitiyFakers;
    private readonly List<Type> _histEntities;
    private readonly List<Type> _histEntitiyFakers;

    public EntityTest()
    {
        _entities = EntityHelpers.GetTestableEntityTypes();
        _entitiyFakers = EntityFakerHelpers.GetTestableEntityFakerTypes();
        _histEntities = HistEntityHelpers.GetTestableHistEntityTypes();
        _histEntitiyFakers = HistEntityFakerHelpers.GetTestableHistEntityFakerTypes();
    }

    public class HistIntegration : EntityTest
    {
        [Fact]
        public void Should_HistCounterpartsExistWithCorrectName()
        {
            foreach (var entityType in _entities)
            {
                var toCut = entityType.Name.IndexOf('`');
                toCut = toCut == -1 ? entityType.Name.Length : toCut;
                var value = $"Hist{entityType.Name.Substring(0, toCut)}";

                var present = _histEntities.Any(x =>
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
            var validEntities = _entities
                .Where(x => x.GetCustomAttribute(typeof(BaseEntityAttribute)) == null)
                .ToList();
            Assert.All(
                validEntities,
                entityType =>
                {
                    // check if IEntityTypeConfiguration exists for this entityType
                    var entityTypeConfiguration =
                        typeof(IEntityTypeConfiguration<>).MakeGenericType(entityType);
                    var entityTypeConfigurationExists = typeof(Entity<,>).Assembly
                        .GetTypes()
                        .Any(x => x.IsAssignableTo(entityTypeConfiguration));

                    Assert.True(
                        entityTypeConfigurationExists,
                        $"IEntityTypeConfiguration<{entityType.Name}> doesn't exist"
                    );
                }
            );
        }

        [Fact]
        public void Should_HistEntityConfigurationExist()
        {
            var validHistEntities = _histEntities
                .Where(x => x.GetCustomAttribute(typeof(BaseHistEntityAttribute)) == null)
                .ToList();
            Assert.All(
                validHistEntities,
                histEntityType =>
                {
                    // check if IEntityTypeConfiguration exists for this entityType
                    var entityTypeConfiguration =
                        typeof(IEntityTypeConfiguration<>).MakeGenericType(histEntityType);
                    var entityTypeConfigurationExists = typeof(HistEntity<>).Assembly
                        .GetTypes()
                        .Any(x => x.IsAssignableTo(entityTypeConfiguration));

                    Assert.True(
                        entityTypeConfigurationExists,
                        $"IEntityTypeConfiguration<{histEntityType.Name}> doesn't exist"
                    );
                }
            );
        }
    }

    public class EntityFakers : EntityTest
    {
        [Fact]
        public void AllEntitiesHaveFakers()
        {
            var entitiesWithoutFakers = _entities
                .Where(
                    x =>
                        !_entitiyFakers.Any(
                            y =>
                                y.BaseType?.GenericTypeArguments.FirstOrDefault() == x
                                && y.Name == x.Name + "Faker"
                        )
                )
                .ToList();
            Assert.Empty(entitiesWithoutFakers);
        }

        [Fact]
        public void AllHistEntitiesHaveFakers()
        {
            var histEntitiesWithoutFakers = _histEntities
                .Where(
                    x =>
                        !_histEntitiyFakers.Any(
                            y =>
                                y.BaseType?.GenericTypeArguments.FirstOrDefault() == x
                                && y.Name == x.Name + "Faker"
                        )
                )
                .ToList();
            Assert.Empty(histEntitiesWithoutFakers);
        }
    }
}
