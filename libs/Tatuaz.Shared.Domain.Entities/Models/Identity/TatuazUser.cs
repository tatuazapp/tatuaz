using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

public class TatuazUser : Entity<HistTatuazUser, string>
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;

    public virtual IEnumerable<TatuazUserRole> TatuazUserRoles { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTatuazUser)base.ToHistEntity(clock, state);
        histEntity.Username = Username;
        histEntity.Email = Email;
        histEntity.PhoneNumber = PhoneNumber;
        return histEntity;
    }
}
