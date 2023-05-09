using System;

namespace Tatuaz.Shared.Domain.Dtos;

[AttributeUsage(validOn: AttributeTargets.Class)]
public class NoUndefAttribute : Attribute { }
