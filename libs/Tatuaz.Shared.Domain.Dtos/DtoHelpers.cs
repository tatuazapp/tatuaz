using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tatuaz.Shared.Domain.Dtos.Dtos.Attributes;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;

namespace Tatuaz.Shared.Domain.Dtos;

public static class DtoHelpers
{
    public static List<Type> GetTestableDtoTypes()
    {
        return typeof(IDto).Assembly
            .GetTypes()
            .Where(
                x =>
                    x.IsAssignableTo(typeof(IDto))
                    && x.GetCustomAttribute(typeof(TestIgnoreDtoAttribute)) == null
            )
            .ToList();
    }
}
