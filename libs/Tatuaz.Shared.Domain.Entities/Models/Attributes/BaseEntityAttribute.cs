using System;

namespace Tatuaz.Shared.Domain.Entities.Models.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class BaseEntityAttribute : Attribute
{
}
