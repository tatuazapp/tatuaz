using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "general");

            migrationBuilder.EnsureSchema(name: "identity");

            migrationBuilder.AlterDatabase().Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "email_info",
                schema: "general",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
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
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_info", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_roles",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        name = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_roles", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_users",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<string>(
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        username = table.Column<string>(
                            type: "character varying(32)",
                            maxLength: 32,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_users", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_user_roles",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        useremail = table.Column<string>(
                            name: "user_email",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        roleid = table.Column<Guid>(name: "role_id", type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_roles_tatuaz_role_id",
                        column: x => x.roleid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id",
                        column: x => x.useremail,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_email_info_recipient_email",
                schema: "general",
                table: "email_info",
                column: "recipient_email"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_role_id",
                schema: "identity",
                table: "tatuaz_user_roles",
                column: "role_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_user_email",
                schema: "identity",
                table: "tatuaz_user_roles",
                column: "user_email"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "email_info", schema: "general");

            migrationBuilder.DropTable(name: "tatuaz_user_roles", schema: "identity");

            migrationBuilder.DropTable(name: "tatuaz_roles", schema: "identity");

            migrationBuilder.DropTable(name: "tatuaz_users", schema: "identity");
        }
    }
}
