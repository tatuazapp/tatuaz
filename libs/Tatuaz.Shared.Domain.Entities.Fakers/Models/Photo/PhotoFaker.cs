using System;
using System.Collections.Generic;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Photo;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Photo;

public sealed class PhotoFaker : Faker<Entities.Models.Photo.Photo>, IEntityFaker
{
    public PhotoFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.ModifiedBy, f => f.Internet.Email());
        RuleFor(x => x.CreatedBy, f => f.Internet.Email());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.PhotoCategories, _ => new List<PhotoCategory>());
    }
}
