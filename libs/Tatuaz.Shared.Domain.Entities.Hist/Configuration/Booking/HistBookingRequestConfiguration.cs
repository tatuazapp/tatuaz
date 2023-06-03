using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Booking;

namespace Tatuaz.Shared.Domain.Entities.Hist.Configuration.Booking;

public class HistBookingRequestConfiguration : IEntityTypeConfiguration<HistBookingRequest>
{
    public void Configure(EntityTypeBuilder<HistBookingRequest> builder)
    {
        builder.ToTable("hist_booking_requests", HistTatuazBookingConstants.SchemaName);
    }
}
