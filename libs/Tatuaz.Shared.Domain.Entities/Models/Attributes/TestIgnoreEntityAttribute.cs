using System;

namespace Tatuaz.Shared.Domain.Entities.Models.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class TestIgnoreEntityAttribute : Attribute { }
