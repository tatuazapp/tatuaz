using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Fakers;

public static class EntityFakerHelpers
{
    public static List<Type> GetTestableEntityFakerTypes()
    {
        return typeof(IEntityFaker).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IEntityFaker))
                    && x.GetCustomAttribute(typeof(TestIgnoreEntityFakerAttribute)) == null
            )
            .ToList();
    }
}
