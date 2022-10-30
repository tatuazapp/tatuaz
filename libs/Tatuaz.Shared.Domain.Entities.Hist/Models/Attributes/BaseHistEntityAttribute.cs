using System;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class BaseHistEntityAttribute : Attribute
{
}
