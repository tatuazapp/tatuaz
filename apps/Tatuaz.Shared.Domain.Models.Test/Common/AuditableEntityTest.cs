﻿using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;
using Tatuaz.Shared.Domain.Models.Test.Generic;

namespace Tatuaz.Shared.Domain.Models.Test.Common;

public class AuditableEntityTest
{
    public class BareGuidAuditableEntity : AuditableEntity<BareGuidHistAuditableEntity, Guid>
    {
    }

    public class BareGuidHistAuditableEntity : AuditableHistEntity<Guid>
    {
    }

    public class BareIntAuditableEntity : AuditableEntity<BareIntAuditableHistEntity, int>
    {
    }

    public class BareIntAuditableHistEntity : AuditableHistEntity<int>
    {
    }

    public class BareStringAuditableEntity : AuditableEntity<BareStringAuditableHistEntity, string>
    {
    }

    public class BareStringAuditableHistEntity : AuditableHistEntity<string>
    {
    }

    public class GuidToHistEntity : GenericToHistEntityTest<BareGuidAuditableEntity, BareGuidHistAuditableEntity, Guid>
    {
        private static readonly BareGuidAuditableEntity[] EntityTestData =
        {
            new()
            {
                Id = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30880"),
                CreatedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30881"),
                CreatedOn = new DateTime(2021, 1, 1),
                ModifiedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30882"),
                ModifiedOn = new DateTime(2021, 1, 2)
            }
        };

        public GuidToHistEntity() : base(EntityTestData)
        {
        }
    }

    public class IntToHistEntity : GenericToHistEntityTest<BareIntAuditableEntity, BareIntAuditableHistEntity, int>
    {
        private static readonly BareIntAuditableEntity[] EntityTestData =
        {
            new()
            {
                Id = 1337,
                CreatedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30881"),
                CreatedOn = new DateTime(2021, 1, 1),
                ModifiedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30882"),
                ModifiedOn = new DateTime(2021, 1, 2)
            }
        };

        public IntToHistEntity() : base(EntityTestData)
        {
        }
    }

    public class
        StringToHistEntity : GenericToHistEntityTest<BareStringAuditableEntity, BareStringAuditableHistEntity, string>
    {
        private static readonly BareStringAuditableEntity[] EntityTestData =
        {
            new()
            {
                Id = "This is lit",
                CreatedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30881"),
                CreatedOn = new DateTime(2021, 1, 1),
                ModifiedBy = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30882"),
                ModifiedOn = new DateTime(2021, 1, 2)
            }
        };

        public StringToHistEntity() : base(EntityTestData)
        {
        }
    }
}