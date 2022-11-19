using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Entities.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities;

public static class EntityHelpers
{
    public static List<Type> GetTestableEntityTypes()
    {
        return typeof(IEntity).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IEntity))
                    && x.GetCustomAttribute(typeof(TestIgnoreEntityAttribute)) == null
            )
            .ToList();
    }
}
