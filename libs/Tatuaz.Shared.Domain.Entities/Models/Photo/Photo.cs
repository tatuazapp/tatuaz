using System;
using System.Collections.Generic;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Photo;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Models.Photo;

public class Photo : AuditableEntity<HistPhoto, Guid>
{
    public Uri Uri => new($"/tatuaz-images/{Id:N}.jpg", UriKind.Relative);

    public virtual ICollection<PhotoCategory> PhotoCategories { get; set; } = default!;
}
