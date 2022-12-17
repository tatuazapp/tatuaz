using System;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class TestIgnoreHistEntityFakerAttribute : Attribute { }
