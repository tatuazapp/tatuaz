using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Common;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Booking;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Identity;
using Tatuaz.Shared.Domain.Entities.Models.Booking;
using Tatuaz.Shared.Domain.Entities.Models.Identity;
using Tatuaz.Shared.Infrastructure.Abstractions.DataAccess;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Booking;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Booking;

public class SendBookingRequestConsumer : TatuazConsumerBase<SendBookingRequest, EmptyDto>
{
    private readonly IUserContext _userContext;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IGenericRepository<
        BookingRequest,
        HistBookingRequest,
        int
    > _bookingRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendBookingRequestConsumer(
        ILogger<SendBookingRequestConsumer> logger,
        IUserContext userContext,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IGenericRepository<BookingRequest, HistBookingRequest, int> bookingRequestRepository,
        IUnitOfWork unitOfWork
    )
        : base(logger)
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _bookingRequestRepository = bookingRequestRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task<TatuazResult<EmptyDto>> ConsumeMessage(
        ConsumeContext<SendBookingRequest> context
    )
    {
        var spec = new FullSpecification<TatuazUser>();
        spec.AddFilter(
            x =>
                x.Username == context.Message.ArtistName
                && x.UserRoles.Any(y => y.Role.Id == TatuazRole.ArtistId)
        );

        var artist = (
            await _userRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
        ).FirstOrDefault();

        if (artist is null)
        {
            return SendBookingRequestResultFactory.ArtistNotFound<EmptyDto>();
        }

        var date = new BookingRequest()
        {
            Status = BookingRequestStatus.Pending,
            Start = context.Message.Start,
            End = context.Message.End,
            Comment = string.IsNullOrWhiteSpace(context.Message.Comment)
                ? null
                : context.Message.Comment,
            ClientEmail = _userContext.RequiredCurrentUserEmail(),
            ArtistEmail = artist.Id,
        };

        _bookingRequestRepository.Create(date);

        await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
