using Tatuaz.Shared.Domain.Models.Common;
using Tatuaz.Shared.Domain.Models.Hist.Common;

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
        public GuidToHistEntity() : base(EntityTestData)
        {
        }

        private static readonly BareGuidEntity[] EntityTestData = {
            new()
            {
                Id = Guid.Parse("8D5EC509-380C-4118-B3F8-C71EB2A30880")
            }
        };
    }

    public class IntToHistEntity : GenericToHistEntityTest<BareIntEntity, BareIntHistEntity, int>
    {
        public IntToHistEntity() : base(EntityTestData)
        {
        }

        private static readonly BareIntEntity[] EntityTestData = {
            new()
            {
                Id = 1337
            }
        };
    }

    public class StringToHistEntity : GenericToHistEntityTest<BareStringEntity, BareStringHistEntity, string>
    {
        public StringToHistEntity() : base(EntityTestData)
        {
        }

        private static readonly BareStringEntity[] EntityTestData = {
            new()
            {
                Id = "This is lit"
            }
        };
    }
}