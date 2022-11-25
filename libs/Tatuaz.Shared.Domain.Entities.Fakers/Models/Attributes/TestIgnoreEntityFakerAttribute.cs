using System;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Attributes;

[AttributeUsage(
    AttributeTargets.Class | AttributeTargets.Interface,
    AllowMultiple = true,
    Inherited = false
)]
public class TestIgnoreEntityFakerAttribute : Attribute { }
