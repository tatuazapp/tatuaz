using System;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

public class HistAward : HistAuditableEntity<Guid>
{
    public string Name { get; set; } = default!;
    public Guid BookId { get; set; }
}