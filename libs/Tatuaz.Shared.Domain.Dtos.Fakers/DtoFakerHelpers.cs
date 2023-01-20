using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Attributes;
using Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos.Fakers;

public static class DtoFakerHelpers
{
    public static List<Type> GetTestableDtoFakerTypes()
    {
        return typeof(IDtoFaker).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IDtoFaker))
                    && x.GetCustomAttribute(typeof(TestIgnoreDtoFakerAttribute)) == null
            )
            .ToList();
    }
}
