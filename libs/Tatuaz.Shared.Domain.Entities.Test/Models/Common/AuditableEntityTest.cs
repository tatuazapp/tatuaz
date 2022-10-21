using NodaTime;
using Tatuaz.Shared.Domain.Entities.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Common;
using Tatuaz.Shared.Domain.Entities.Test.Generic;

namespace Tatuaz.Shared.Domain.Entities.Test.Common;

public class AuditableEntityTest
{
    public class BareGuidAuditableEntity : AuditableEntity<BareGuidHistAuditableEntity, Guid> { }

    public class BareGuidHistAuditableEntity : AuditableHistEntity<Guid> { }

    public class BareIntAuditableEntity : AuditableEntity<BareIntAuditableHistEntity, int> { }

    public class BareIntAuditableHistEntity : AuditableHistEntity<int> { }

    public class BareStringAuditableEntity
        : AuditableEntity<BareStringAuditableHistEntity, string> { }

    public class BareStringAuditableHistEntity : AuditableHistEntity<string> { }

    public class GuidToHistEntity
        : GenericToHistEntityTest<BareGuidAuditableEntity, BareGuidHistAuditableEntity, Guid>
    {
        private static readonly BareGuidAuditableEntity[] EntityTestData =
        {
            new()
            {
                Id = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30880"),
                CreatedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30881"),
                CreatedOn = Instant.FromUtc(2021, 1, 1, 1, 1, 1),
                ModifiedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30882"),
                ModifiedOn = Instant.FromUtc(2021, 1, 1, 1, 1, 2)
            }
        };

        public GuidToHistEntity(IClock clock) : base(clock, EntityTestData) { }
    }

    public class IntToHistEntity
        : GenericToHistEntityTest<BareIntAuditableEntity, BareIntAuditableHistEntity, int>
    {
        private static readonly BareIntAuditableEntity[] EntityTestData =
        {
            new()
            {
                Id = 1337,
                CreatedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30881"),
                CreatedOn = Instant.FromUtc(2021, 1, 1, 1, 1, 1),
                ModifiedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30882"),
                ModifiedOn = Instant.FromUtc(2021, 1, 1, 1, 1, 2)
            }
        };

        public IntToHistEntity(IClock clock) : base(clock, EntityTestData) { }
    }

    public class StringToHistEntity
        : GenericToHistEntityTest<BareStringAuditableEntity, BareStringAuditableHistEntity, string>
    {
        private static readonly BareStringAuditableEntity[] EntityTestData =
        {
            new()
            {
                Id = "This is lit",
                CreatedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30881"),
                CreatedOn = Instant.FromUtc(2021, 1, 1, 1, 1, 1),
                ModifiedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30882"),
                ModifiedOn = Instant.FromUtc(2021, 1, 1, 1, 1, 2)
            }
        };

        public StringToHistEntity(IClock clock) : base(clock, EntityTestData) { }
    }
}
