using Bogus;
using NodaTime.Extensions;
using Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Post;
using Tatuaz.Shared.Domain.Entities.Models.Post;

namespace Tatuaz.Shared.Domain.Entities.Hist.Fakers.Models.Post;

public sealed class HistCommentFaker : Faker<HistComment>, IHistEntityFaker
{
    public HistCommentFaker()
    {
        StrictMode(true);
        RuleFor(x => x.HistId, f => f.Random.Guid());
        RuleFor(x => x.HistState, f => f.PickRandom<HistState>());
        RuleFor(x => x.HistDumpedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.ModifiedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.ModifiedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.CreatedBy, f => f.Random.Guid().ToString());
        RuleFor(x => x.CreatedAt, f => f.Date.Past().ToUniversalTime().ToInstant());
        RuleFor(x => x.Id, f => f.Random.Guid());
        RuleFor(x => x.ParentCommentId, f => f.Random.Guid());
        RuleFor(x => x.PostId, f => f.Random.Guid());
        RuleFor(x => x.UserId, f => f.Random.Guid().ToString());
        RuleFor(x => x.Content, f => f.Lorem.Paragraph());
    }
}
