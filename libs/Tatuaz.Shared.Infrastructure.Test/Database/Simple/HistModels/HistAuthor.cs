using Tatuaz.Shared.Domain.Models.Hist.Common;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

public class HistAuthor : AuditableHistEntity<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
