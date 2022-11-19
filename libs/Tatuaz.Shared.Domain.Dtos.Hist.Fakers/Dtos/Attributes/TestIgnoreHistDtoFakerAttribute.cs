using System;

namespace Tatuaz.Shared.Domain.Dtos.Hist.Fakers.Dtos.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class TestIgnoreHistDtoFakerAttribute : Attribute { }
