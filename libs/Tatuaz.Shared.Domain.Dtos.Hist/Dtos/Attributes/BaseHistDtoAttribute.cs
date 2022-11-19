using System;

namespace Tatuaz.Shared.Domain.Dtos.Hist.Dtos.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class BaseHistDtoAttribute : Attribute { }
