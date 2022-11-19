using System;

namespace Tatuaz.Shared.Domain.Dtos.Fakers.Dtos.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class TestIgnoreDtoFakerAttribute : Attribute { }
