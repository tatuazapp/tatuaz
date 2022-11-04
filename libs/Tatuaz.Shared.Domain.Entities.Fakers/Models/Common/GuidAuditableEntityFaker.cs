using System;
using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Models.Common;

namespace Tatuaz.Shared.Domain.Entities.Fakers.Models.Common;

public sealed class GuidAuditableEntityFaker : Faker<AuditableEntity<HistAuditableEntity<Guid>, Guid>>
{
    public GuidAuditableEntityFaker()
    {
        StrictMode(true);
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt,
            f => f.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-1)).ToUniversalTime().ToInstant());
        RuleFor(x => x.ModifiedAt,
            f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now).ToUniversalTime().ToInstant());
    }
}