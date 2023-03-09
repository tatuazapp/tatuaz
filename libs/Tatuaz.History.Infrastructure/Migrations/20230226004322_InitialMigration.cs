using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "H_photo");

            migrationBuilder.EnsureSchema(name: "H_general");

            migrationBuilder.EnsureSchema(name: "H_identity");

            migrationBuilder.AlterDatabase().Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "H_categories",
                schema: "H_photo",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        title = table.Column<string>(
                            type: "character varying(64)",
                            maxLength: 64,
                            nullable: false
                        ),
                        type = table.Column<int>(type: "integer", nullable: false),
                        imageuri = table.Column<string>(
                            name: "image_uri",
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: false
                        ),
                        popularity = table.Column<int>(type: "integer", nullable: false),
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
                        id = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_categories", x => x.histid);
                }
            );

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
                name: "H_photo_categories",
                schema: "H_photo",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        photoid = table.Column<Guid>(
                            name: "photo_id",
                            type: "uuid",
                            nullable: false
                        ),
                        categoryid = table.Column<int>(
                            name: "category_id",
                            type: "integer",
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
                    table.PrimaryKey("pk_h_photo_categories", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_photos",
                schema: "H_photo",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
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
                        ),
                        modifiedby = table.Column<string>(
                            name: "modified_by",
                            type: "text",
                            nullable: false
                        ),
                        modifiedat = table.Column<Instant>(
                            name: "modified_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        createdby = table.Column<string>(
                            name: "created_by",
                            type: "text",
                            nullable: false
                        ),
                        createdat = table.Column<Instant>(
                            name: "created_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_photos", x => x.id);
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
                        auth0id = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        ),
                        foregroundphotoid = table.Column<Guid>(
                            name: "foreground_photo_id",
                            type: "uuid",
                            nullable: true
                        ),
                        backgroundphotoid = table.Column<Guid>(
                            name: "background_photo_id",
                            type: "uuid",
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

            migrationBuilder.CreateTable(
                name: "H_user_categories",
                schema: "H_photo",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        userid = table.Column<string>(
                            name: "user_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        categoryid = table.Column<int>(
                            name: "category_id",
                            type: "integer",
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
                        id = table.Column<int>(type: "integer", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_user_categories", x => x.histid);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "H_categories", schema: "H_photo");

            migrationBuilder.DropTable(name: "H_email_info", schema: "H_general");

            migrationBuilder.DropTable(name: "H_photo_categories", schema: "H_photo");

            migrationBuilder.DropTable(name: "H_photos", schema: "H_photo");

            migrationBuilder.DropTable(name: "H_tatuaz_roles", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_tatuaz_user_roles", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_tatuaz_users", schema: "H_identity");

            migrationBuilder.DropTable(name: "H_user_categories", schema: "H_photo");
        }
    }
}
