using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

public class Book : AuditableEntity<HistBook, Guid>
{
    public string Title { get; set; } = default!;
    public Guid AuthorId { get; set; }
    public virtual Author Author { get; set; } = default!;
    public virtual IEnumerable<Award> Awards { get; set; } = default!;

    public override HistBook ToHistEntity(IClock clock, HistState state)
    {
        var histBook = (HistBook)base.ToHistEntity(clock, state);
        histBook.Title = Title;
        return histBook;
    }
}
