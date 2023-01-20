using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist;

public static class HistEntityHelpers
{
    public static List<Type> GetTestableHistEntityTypes()
    {
        return typeof(IHistEntity).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IHistEntity))
                    && x.GetCustomAttribute(typeof(TestIgnoreHistEntityAttribute)) == null
            )
            .ToList();
    }
}
