using System;

namespace Tatuaz.Shared.Domain.Dtos.Dtos.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class BaseDtoAttribute : Attribute { }
