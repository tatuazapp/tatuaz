using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "H_general");

            migrationBuilder.EnsureSchema(name: "H_identity");

            migrationBuilder
                .AlterDatabase()
                .Annotation("Npgsql:Enum:hist_state", "added,modified,deleted")
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "H_cities",
                schema: "H_general",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        name = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        ),
                        timezoneid = table.Column<Guid>(
                            name: "time_zone_id",
                            type: "uuid",
                            nullable: false
                        ),
                        location = table.Column<Point>(type: "geography (point)", nullable: false),
                        country = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<HistState>(
                            name: "hist_state",
                            type: "hist_state",
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
                    table.PrimaryKey("pk_h_cities", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_tatuaz_roles",
                schema: "H_identity",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        name = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        ),
                        histstate = table.Column<HistState>(
                            name: "hist_state",
                            type: "hist_state",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        id = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_tatuaz_roles", x => x.histid);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_tatuaz_user_roles",
                schema: "H_identity",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        tatuazuserid = table.Column<string>(
                            name: "tatuaz_user_id",
                            type: "text",
                            nullable: false
                        ),
                        tatuazroleid = table.Column<Guid>(
                            name: "tatuaz_role_id",
                            type: "uuid",
                            nullable: false
                        ),
                        histstate = table.Column<HistState>(
                            name: "hist_state",
                            type: "hist_state",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        id = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_tatuaz_user_roles", x => x.histid);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_tatuaz_users",
                schema: "H_identity",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        username = table.Column<string>(
                            type: "character varying(32)",
                            maxLength: 32,
                            nullable: false
                        ),
                        email = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: false
                        ),
                        phonenumber = table.Column<string>(
                            name: "phone_number",
                            type: "character varying(64)",
                            maxLength: 64,
                            nullable: true
                        ),
                        histstate = table.Column<HistState>(
                            name: "hist_state",
                            type: "hist_state",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        id = table.Column<string>(type: "text", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_tatuaz_users", x => x.histid);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_time_zones",
                schema: "H_general",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        name = table.Column<string>(
                            type: "character varying(64)",
                            maxLength: 64,
                            nullable: false
                        ),
                        offsetfromutc = table.Column<int>(
                            name: "offset_from_utc",
                            type: "integer",
                            nullable: false
                        ),
                        description = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<HistState>(
                            name: "hist_state",
                            type: "hist_state",
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
                    table.PrimaryKey("pk_h_time_zones", x => x.id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_h_cities_location",
                schema: "H_general",
                table: "H_cities",
                column: "location",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_h_cities_name",
                schema: "H_general",
                table: "H_cities",
                column: "name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_h_time_zones_name",
                schema: "H_general",
                table: "H_time_zones",
                column: "name",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "H_cities", schema: "H_general");

            migrationBuilder.DropTable(name: "H_tatuaz_roles", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_tatuaz_user_roles", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_tatuaz_users", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_time_zones", schema: "H_general");
        }
    }
}
