using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Dtos.Hist.Dtos.Attributes;
using Tatuaz.Shared.Domain.Dtos.Hist.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Hist;

public static class HistDtoHelpers
{
    public static List<Type> GetTestableHistDtoTypes()
    {
        return typeof(IHistDto).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IHistDto))
                    && x.GetCustomAttribute(typeof(TestIgnoreHistDtoAttribute)) == null
            )
            .ToList();
    }
}
