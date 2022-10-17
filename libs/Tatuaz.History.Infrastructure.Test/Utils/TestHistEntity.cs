using Tatuaz.Shared.Domain.Entities.Hist.Common;

namespace Tatuaz.History.DataAccess.Test.Utils;

public class TestHistEntity : HistEntity<Guid>
{
    public string Name { get; set; } = default!;
}
