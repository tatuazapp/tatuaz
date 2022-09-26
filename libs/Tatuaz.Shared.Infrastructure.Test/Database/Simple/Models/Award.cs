using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

public class Award : AuditableEntity<HistAward, Guid>
{
    public string Name { get; set; } = default!;
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; } = default!;

    public override HistAward ToHistEntity()
    {
        var histAward = base.ToHistEntity();
        histAward.Name = Name;
        return histAward;
    }
}
