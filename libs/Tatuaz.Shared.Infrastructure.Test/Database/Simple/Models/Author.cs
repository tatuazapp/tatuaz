using NodaTime;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

public class Author : AuditableEntity<HistAuthor, Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public virtual IEnumerable<Book> Books { get; set; }

    public override HistAuthor ToHistEntity(IClock clock)
    {
        var histAuthor = (HistAuthor)base.ToHistEntity(clock);
        histAuthor.FirstName = FirstName;
        histAuthor.LastName = LastName;
        return histAuthor;
    }
}
