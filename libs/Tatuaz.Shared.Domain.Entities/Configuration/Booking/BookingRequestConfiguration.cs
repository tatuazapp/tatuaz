using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tatuaz.Shared.Domain.Entities.Models.Booking;

namespace Tatuaz.Shared.Domain.Entities.Configuration.Booking;

public class BookingRequestConfiguration : IEntityTypeConfiguration<BookingRequest>
{
    public void Configure(EntityTypeBuilder<BookingRequest> builder)
    {
        builder.ToTable("booking_requests", TatuazBookingConstants.SchemaName);

        builder
            .HasOne(x => x.Client)
            .WithMany(x => x.BookingRequests)
            .HasForeignKey(x => x.ClientEmail);

        builder.HasOne(x => x.Artist).WithMany().HasForeignKey(x => x.ArtistEmail);
    }
}
