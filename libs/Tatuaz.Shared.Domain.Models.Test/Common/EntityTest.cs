using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;
using Tatuaz.Shared.Domain.Models.Test.Generic;

namespace Tatuaz.Shared.Domain.Models.Test.Common;

public class EntityTest
{
    public class BareGuidEntity : Entity<BareGuidHistEntity, Guid>
    {
    }

    public class BareGuidHistEntity : HistEntity<Guid>
    {
    }

    public class BareIntEntity : Entity<BareIntHistEntity, int>
    {
    }

    public class BareIntHistEntity : HistEntity<int>
    {
    }

    public class BareStringEntity : Entity<BareStringHistEntity, string>
    {
    }

    public class BareStringHistEntity : HistEntity<string>
    {
    }

    public class GuidToHistEntity : GenericToHistEntityTest<BareGuidEntity, BareGuidHistEntity, Guid>
    {
        private static readonly BareGuidEntity[] EntityTestData =
        {
            new()
            {
                Id = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30880")
            }
        };

        public GuidToHistEntity() : base(EntityTestData)
        {
        }
    }

    public class IntToHistEntity : GenericToHistEntityTest<BareIntEntity, BareIntHistEntity, int>
    {
        private static readonly BareIntEntity[] EntityTestData =
        {
            new()
            {
                Id = 1337
            }
        };

        public IntToHistEntity() : base(EntityTestData)
        {
        }
    }

    public class StringToHistEntity : GenericToHistEntityTest<BareStringEntity, BareStringHistEntity, string>
    {
        private static readonly BareStringEntity[] EntityTestData =
        {
            new()
            {
                Id = "This is lit"
            }
        };

        public StringToHistEntity() : base(EntityTestData)
        {
        }
    }
}
