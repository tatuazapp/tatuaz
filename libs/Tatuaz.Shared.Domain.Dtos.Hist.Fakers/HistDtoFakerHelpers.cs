using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Dtos.Hist.Fakers.Dtos.Attributes;
using Tatuaz.Shared.Domain.Dtos.Hist.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Hist.Fakers;

public static class HistDtoFakerHelpers
{
    public static List<Type> GetTestableHistDtoFakerTypes()
    {
        return typeof(IHistDtoFaker).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IHistDtoFaker))
                    && x.GetCustomAttribute(typeof(TestIgnoreHistDtoFakerAttribute)) == null
            )
            .ToList();
    }
}
