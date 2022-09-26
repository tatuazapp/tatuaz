using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Infrastructure.Test.Database.Simple.HistModels;

namespace Tatuaz.Shared.Infrastructure.Test.Database.Simple.Models;

public class Author : AuditableEntity<HistAuthor, Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public virtual IEnumerable<Book> Books { get; set; }

    public override HistAuthor ToHistEntity()
    {
        var histAuthor = base.ToHistEntity();
        histAuthor.FirstName = FirstName;
        histAuthor.LastName = LastName;
        return histAuthor;
    }
}
