using NodaTime;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

public class Award : AuditableEntity<HistAward, Guid>
{
    public string Name { get; set; } = default!;
    public Guid BookId { get; set; }
    public virtual Book Book { get; set; } = default!;

    public override HistAward ToHistEntity(IClock clock)
    {
        var histAward = (HistAward)base.ToHistEntity(clock);
        histAward.Name = Name;
        return histAward;
    }
}