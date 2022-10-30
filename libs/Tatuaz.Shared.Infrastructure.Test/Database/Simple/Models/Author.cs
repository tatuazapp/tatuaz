using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

public class Author : AuditableEntity<HistAuthor, Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public virtual IEnumerable<Book> Books { get; set; } = default!;

    public override HistAuthor ToHistEntity(IClock clock, HistState state)
    {
        var histAuthor = (HistAuthor)base.ToHistEntity(clock, state);
        histAuthor.FirstName = FirstName;
        histAuthor.LastName = LastName;
        return histAuthor;
    }
}
