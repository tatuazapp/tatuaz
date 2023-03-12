using System;
using System.Collections.Generic;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Post;

public class InitialPost : AuditableEntity<HistInitialPost, Guid>, IEntity
{
    public ICollection<InitialPostPhoto> InitialPostPhotos { get; set; } = default!;

    public override HistEntity<Guid> ToHistEntity(IClock clock, HistState state)
    {
        var histEntity = (HistInitialPost)base.ToHistEntity(clock, state);
        return histEntity;
    }
}
