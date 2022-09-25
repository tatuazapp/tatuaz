using System.ComponentModel.DataAnnotations;

namespace Tatuaz.Shared.Domain.Models.Hist.Common;

public class HistEntity<TId>
    where TId : notnull
{
    // TODO: add index
    public TId Id { get; set; } = default!;

    [Key] public Guid HistId { get; set; }

    // TODO: add index
    public DateTime HistFrom { get; set; }

    // TODO: add index
    public DateTime? HistTo { get; set; }
}
