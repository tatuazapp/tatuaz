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
using Tatuaz.Shared.Infrastructure.Abstractions.Paging;
using Tatuaz.Shared.Infrastructure.Specification;
using Tatuaz.Shared.Pipeline.Factories.Results;
using Tatuaz.Shared.Pipeline.Factories.Results.Booking;
using Tatuaz.Shared.Pipeline.Messages;
using Tatuaz.Shared.Pipeline.Queues;
using Tatuaz.Shared.Pipeline.UserContext;

namespace Tatuaz.Dashboard.Queue.Consumers.Booking;

public class RespondToBookingRequestConsumer : TatuazConsumerBase<RespondToBookingRequest, EmptyDto>
{
    private readonly IUserContext _userContext;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IGenericRepository<
        BookingRequest,
        HistBookingRequest,
        int
    > _bookingRequestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RespondToBookingRequestConsumer(
        ILogger<RespondToBookingRequestConsumer> logger,
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
        ConsumeContext<RespondToBookingRequest> context
    )
    {
        var isArtist = await _userRepository
            .ExistsByPredicateAsync(
                x =>
                    x.Id == _userContext.RequiredCurrentUserEmail()
                    && x.UserRoles.Any(y => y.Role.Id == TatuazRole.ArtistId)
            )
            .ConfigureAwait(false);

        if (!isArtist)
        {
            return RespondToBookingRequestErrorResultFactory.NotArtist<EmptyDto>();
        }

        var spec = new FullSpecification<BookingRequest>();
        spec.AddFilter(
            x =>
                x.Id == context.Message.BookingRequestId
                && x.ArtistEmail == _userContext.RequiredCurrentUserEmail()
        );
        spec.TrackingStrategy = TrackingStrategy.Tracking;

        var bookingRequest = (
            await _bookingRequestRepository.GetBySpecificationAsync(spec).ConfigureAwait(false)
        ).FirstOrDefault();

        if (bookingRequest == null)
        {
            return RespondToBookingRequestErrorResultFactory.BookingRequestNotFound<EmptyDto>();
        }

        if (bookingRequest.Status != BookingRequestStatus.Pending)
        {
            return RespondToBookingRequestErrorResultFactory.BookingRequestNotPending<EmptyDto>();
        }

        if (context.Message.Accept)
        {
            bookingRequest.Status = BookingRequestStatus.Accepted;
        }
        else
        {
            bookingRequest.Status = BookingRequestStatus.Rejected;
        }

        await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

        return CommonResultFactory.Ok(new EmptyDto());
    }
}
