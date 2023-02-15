using System;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using NodaTime;
using Tatuaz.Dashboard.Emails.Data;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.General;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Dashboard.Emails.EmailHandlers;

public class TestEmailHandler : EmailHandlerBase
{
    public TestEmailHandler(
        IFluentEmail fluentEmail,
        ILogger logger,
        IGenericRepository<EmailInfo, HistEmailInfo, Guid> emailInfoRepository,
        IUnitOfWork unitOfWork,
        IClock clock
    )
        : base(fluentEmail, logger, emailInfoRepository, unitOfWork, clock) { }

    protected override Task<EmailData> GetEmailData(Guid objectId)
    {
        return Task.FromResult(
            new EmailData("Test name", "Test subject", "test", new { User = "Haha brrr" })
        );
    }

    public override string GetEmailType()
    {
        return "Test";
    }
}
