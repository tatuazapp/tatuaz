using System;

namespace Tatuaz.Shared.Domain.Entities.Hist.Models.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class TestIgnoreHistEntityAttribute : Attribute { }
