using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Models.Identity;

/// <summary>
/// class representing user PK is email;
/// </summary>
public class TatuazUser : Entity<HistTatuazUser, string>
{
    public string Username { get; set; } = default!;
    public string Auth0Id { get; set; } = default!;
    public virtual ICollection<TatuazUserRole> UserRoles { get; set; } = default!;
    public virtual ICollection<UserCategory> UserPhotoCategories { get; set; } = default!;
    public Guid? ForegroundPhotoId { get; set; }
    public virtual Photo.Photo? ForegroundPhoto { get; set; }
    public Guid? BackgroundPhotoId { get; set; }
    public virtual Photo.Photo? BackgroundPhoto { get; set; }
    public string? Bio { get; set; } = default!;

    public override HistEntity ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistTatuazUser)base.ToHistEntity(clock, state);
        histEntity.Username = Username;
        histEntity.Auth0Id = Auth0Id;
        histEntity.ForegroundPhotoId = ForegroundPhotoId;
        histEntity.BackgroundPhotoId = BackgroundPhotoId;
        histEntity.Bio = Bio;
        return histEntity;
    }
}
