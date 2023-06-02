using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "hist_dates", schema: "H_booking");

            migrationBuilder.CreateTable(
                name: "hist_booking_requests",
                schema: "H_booking",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        status = table.Column<int>(type: "integer", nullable: false),
                        start = table.Column<Instant>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        end = table.Column<Instant>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        comment = table.Column<string>(type: "text", nullable: true),
                        customeremail = table.Column<string>(
                            name: "customer_email",
                            type: "text",
                            nullable: false
                        ),
                        artistemail = table.Column<string>(
                            name: "artist_email",
                            type: "text",
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hist_booking_requests", x => x.id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "hist_booking_requests", schema: "H_booking");

            migrationBuilder.CreateTable(
                name: "hist_dates",
                schema: "H_booking",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        artistemail = table.Column<string>(
                            name: "artist_email",
                            type: "text",
                            nullable: false
                        ),
                        comment = table.Column<string>(type: "text", nullable: true),
                        customeremail = table.Column<string>(
                            name: "customer_email",
                            type: "text",
                            nullable: false
                        ),
                        end = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        start = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        status = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hist_dates", x => x.id);
                }
            );
        }
    }
}
