using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

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
                name: "H_email_info",
                schema: "H_general",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        recipientemail = table.Column<string>(
                            name: "recipient_email",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        emailtype = table.Column<string>(
                            name: "email_type",
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        objectid = table.Column<Guid>(
                            name: "object_id",
                            type: "uuid",
                            nullable: false
                        ),
                        orderedat = table.Column<Instant>(
                            name: "ordered_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        sentat = table.Column<Instant>(
                            name: "sent_at",
                            type: "timestamp with time zone",
                            nullable: true
                        ),
                        retrycount = table.Column<int>(
                            name: "retry_count",
                            type: "integer",
                            nullable: false
                        ),
                        error = table.Column<string>(
                            type: "character varying(500)",
                            maxLength: 500,
                            nullable: true
                        ),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
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
                    table.PrimaryKey("pk_h_email_info", x => x.histid);
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
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
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
                        useremail = table.Column<string>(
                            name: "user_email",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        roleid = table.Column<Guid>(name: "role_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
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
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        id = table.Column<string>(
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_tatuaz_users", x => x.histid);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "H_email_info", schema: "H_general");

            migrationBuilder.DropTable(name: "H_tatuaz_roles", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_tatuaz_user_roles", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_tatuaz_users", schema: "H_identity");
        }
    }
}
