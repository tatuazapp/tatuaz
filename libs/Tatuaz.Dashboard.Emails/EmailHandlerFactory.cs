using System;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using NodaTime;
using Tatuaz.Dashboard.Emails.EmailHandlers;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.General;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Dashboard.Emails;

public class EmailHandlerFactory : IEmailHandlerFactory
{
    private readonly IFluentEmail _fluentEmail;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IGenericRepository<EmailInfo, HistEmailInfo, Guid> _emailInfoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClock _clock;

    public EmailHandlerFactory(
        IFluentEmail fluentEmail,
        ILoggerFactory loggerFactory,
        IGenericRepository<EmailInfo, HistEmailInfo, Guid> emailInfoRepository,
        IUnitOfWork unitOfWork,
        IClock clock
    )
    {
        _fluentEmail = fluentEmail;
        _loggerFactory = loggerFactory;
        _emailInfoRepository = emailInfoRepository;
        _unitOfWork = unitOfWork;
        _clock = clock;
    }

    public IEmailHandler GetEmailHandler(EmailType emailType)
    {
        return emailType switch
        {
            EmailType.Test
                => new TestEmailHandler(
                    _fluentEmail,
                    _loggerFactory.CreateLogger<TestEmailHandler>(),
                    _emailInfoRepository,
                    _unitOfWork,
                    _clock
                ),
            _
                => throw new ArgumentOutOfRangeException(
                    nameof(emailType),
                    emailType,
                    "Email handler not found"
                )
        };
    }
}
