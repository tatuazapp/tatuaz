using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NodaTime;
using Tatuaz.Dashboard.Emails.Data;
using Tatuaz.Dashboard.Emails.Exceptions;
using Tatuaz.Shared.Domain.Entities.Hist.Models.General;
using Tatuaz.Shared.Domain.Entities.Models.General;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;

namespace Tatuaz.Dashboard.Emails.EmailHandlers;

public abstract class EmailHandlerBase : IEmailHandler
{
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger _logger;
    private readonly IGenericRepository<EmailInfo, HistEmailInfo, Guid> _emailInfoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClock _clock;

    public EmailHandlerBase(
        IFluentEmail fluentEmail,
        ILogger logger,
        IGenericRepository<EmailInfo, HistEmailInfo, Guid> emailInfoRepository,
        IUnitOfWork unitOfWork,
        IClock clock
    )
    {
        _fluentEmail = fluentEmail;
        _logger = logger;
        _emailInfoRepository = emailInfoRepository;
        _unitOfWork = unitOfWork;
        _clock = clock;
    }

    protected abstract Task<EmailData> GetEmailData(Guid objectId);

    public async Task SendEmailAsync(Guid emailId, string recipientEmail, Guid objectId)
    {
        try
        {
            var emailInfo = await _emailInfoRepository.GetByIdAsync(emailId).ConfigureAwait(false);
            if (emailInfo == null)
            {
                emailInfo = new EmailInfo()
                {
                    Id = emailId,
                    RecipientEmail = recipientEmail,
                    EmailType = GetType().Name,
                    ObjectId = objectId,
                    SentAt = null,
                    RetryCount = 0,
                    Error = null
                };
                _emailInfoRepository.Create(emailInfo);
            }
            else
            {
                emailInfo.RetryCount++;
            }

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            var emailData = await GetEmailData(objectId).ConfigureAwait(false);
            await _fluentEmail
                .To(recipientEmail, emailData.RecipientName)
                .Subject(emailData.Subject)
                .UsingTemplateFromEmbedded(
                    GetEmailTemplatePath(emailData.TemplateName),
                    emailData.TemplateData,
                    Assembly.GetExecutingAssembly()
                )
                .SendAsync()
                .ConfigureAwait(false);

            emailInfo.SentAt = _clock.GetCurrentInstant();
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
        catch (DbUpdateException)
        {
            throw;
        }
        catch (Exception e)
        {
            _logger.LogError(
                e,
                "Error while sending email to {RecipientEmail} about {ObjectId}",
                recipientEmail,
                objectId.ToString()
            );
            throw new SendEmailException();
        }
    }

    public abstract string GetEmailType();

    private static string GetEmailTemplatePath(string templateName)
    {
        var tmp =
            $"{Assembly.GetExecutingAssembly().GetName().Name}.Templates.{templateName}.liquid";
        return tmp;
    }
}
