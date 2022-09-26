using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

public class HistAward : AuditableHistEntity<Guid>
{
    public string Name { get; set; } = default!;
    public Guid BookId { get; set; }
}
