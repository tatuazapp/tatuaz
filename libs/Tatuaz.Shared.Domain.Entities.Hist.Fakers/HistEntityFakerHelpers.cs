using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Attributes;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers;

public static class HistEntityFakerHelpers
{
    public static List<Type> GetTestableHistEntityFakerTypes()
    {
        return typeof(IHistEntityFaker).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IHistEntityFaker))
                    && x.GetCustomAttribute(typeof(TestIgnoreHistEntityFakerAttribute)) == null
            )
            .ToList();
    }
}
