using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

public class HistBook : HistAuditableEntity<Guid>
{
    public string Title { get; set; } = default!;
}
