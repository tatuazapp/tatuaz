using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

/// <summary>
/// class representing user PK is email;
/// </summary>
public class TatuazUser : Entity<HistTatuazUser, string>
{
    public string Username { get; set; } = default!;
    public string Auth0Id { get; set; } = default!;
    public ICollection<TatuazUserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<UserPhotoCategory> UserPhotoCategories { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTatuazUser)base.ToHistEntity(clock, state);
        histEntity.Username = Username;
        histEntity.Auth0Id = Auth0Id;
        return histEntity;
    }
}
