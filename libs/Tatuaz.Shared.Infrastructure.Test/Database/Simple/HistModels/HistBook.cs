using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

public class HistBook : AuditableHistEntity<Guid>
{
    public string Title { get; set; } = default!;
}