using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tatuaz.Dashboard.Queue.Contracts.Booking;
using Tatuaz.Shared.Domain.Dtos.Dtos.Booking;
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

public class ListIncomingBookingRequestsConsumer
    : TatuazConsumerBase<ListIncomingBookingRequests, PagedData<BookingRequestDto>>
{
    private readonly IGenericRepository<
        BookingRequest,
        HistBookingRequest,
        int
    > _bookingRequestRepository;
    private readonly IGenericRepository<TatuazUser, HistTatuazUser, string> _userRepository;
    private readonly IUserContext _userContext;

    public ListIncomingBookingRequestsConsumer(
        ILogger<ListMyBookingRequestsConsumer> logger,
        IGenericRepository<BookingRequest, HistBookingRequest, int> bookingRequestRepository,
        IGenericRepository<TatuazUser, HistTatuazUser, string> userRepository,
        IUserContext userContext
    )
        : base(logger)
    {
        _bookingRequestRepository = bookingRequestRepository;
        _userRepository = userRepository;
        _userContext = userContext;
    }

    protected override async Task<TatuazResult<PagedData<BookingRequestDto>>> ConsumeMessage(
        ConsumeContext<ListIncomingBookingRequests> context
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
            return ListIncomingBookingRequestsErrorResultFactory.NotArtist<
                PagedData<BookingRequestDto>
            >();
        }

        var spec = new FullSpecification<BookingRequest>();
        spec.AddFilter(
            x =>
                x.ArtistEmail == _userContext.RequiredCurrentUserEmail()
                && x.Status == context.Message.Status
        );
        spec.AddOrder(x => x.Start, OrderDirection.Descending);
        spec.UseInclude(x => x.Include(x => x.Artist));

        var result = await _bookingRequestRepository
            .GetBySpecificationWithPagingAsync(
                spec,
                new PagedParams(context.Message.PageNumber, context.Message.PageSize)
            )
            .ConfigureAwait(false);

        return CommonResultFactory.Ok(
            new PagedData<BookingRequestDto>(
                result.Data.Select(
                    x =>
                        new BookingRequestDto(
                            x.Id,
                            x.Artist.Username,
                            x.Start.ToDateTimeUtc(),
                            x.End.ToDateTimeUtc(),
                            x.Comment,
                            x.Status
                        )
                ),
                result.PageNumber,
                result.PageSize,
                result.TotalPages,
                result.TotalCount
            )
        );
    }
}
