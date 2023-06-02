using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "booking");

            migrationBuilder.CreateTable(
                name: "dates",
                schema: "booking",
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
                        start = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        end = table.Column<DateTime>(
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        comment = table.Column<string>(type: "text", nullable: true),
                        clientemail = table.Column<string>(
                            name: "client_email",
                            type: "character varying(320)",
                            nullable: false
                        ),
                        artistemail = table.Column<string>(
                            name: "artist_email",
                            type: "character varying(320)",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dates", x => x.id);
                    table.ForeignKey(
                        name: "fk_dates_tatuaz_users_artist_id",
                        column: x => x.artistemail,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_dates_tatuaz_users_client_id",
                        column: x => x.clientemail,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_dates_artist_email",
                schema: "booking",
                table: "dates",
                column: "artist_email"
            );

            migrationBuilder.CreateIndex(
                name: "ix_dates_client_email",
                schema: "booking",
                table: "dates",
                column: "client_email"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "dates", schema: "booking");
        }
    }
}
