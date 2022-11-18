using System;
using System.Resources;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Tatuaz.Shared.Helpers;

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
                        id = table.Column<string>(type: "text", nullable: false),
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
                            type: "character varying(16)",
                            maxLength: 16,
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_users", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "time_zones",
                schema: "general",
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
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_time_zones", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_user_roles",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        tatuazuserid = table.Column<string>(
                            name: "tatuaz_user_id",
                            type: "text",
                            nullable: false
                        ),
                        tatuazroleid = table.Column<Guid>(
                            name: "tatuaz_role_id",
                            type: "uuid",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_roles_tatuaz_role_id",
                        column: x => x.tatuazroleid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id",
                        column: x => x.tatuazuserid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "cities",
                schema: "general",
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
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                    table.ForeignKey(
                        name: "fk_cities_time_zone_time_zone_id",
                        column: x => x.timezoneid,
                        principalSchema: "general",
                        principalTable: "time_zones",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_cities_location",
                schema: "general",
                table: "cities",
                column: "location",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_cities_name",
                schema: "general",
                table: "cities",
                column: "name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_cities_time_zone_id",
                schema: "general",
                table: "cities",
                column: "time_zone_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_tatuaz_role_id",
                schema: "identity",
                table: "tatuaz_user_roles",
                column: "tatuaz_role_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_tatuaz_user_id",
                schema: "identity",
                table: "tatuaz_user_roles",
                column: "tatuaz_user_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_time_zones_name",
                schema: "general",
                table: "time_zones",
                column: "name",
                unique: true
            );

            migrationBuilder.Sql(
                MigrationHelpers.ReadSql(
                    typeof(InitialMigration),
                    "20221118201629_InitialMigration.sql"
                )
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "cities", schema: "general");

            migrationBuilder.DropTable(name: "tatuaz_user_roles", schema: "identity");

            migrationBuilder.DropTable(name: "time_zones", schema: "general");

            migrationBuilder.DropTable(name: "tatuaz_roles", schema: "identity");

            migrationBuilder.DropTable(name: "tatuaz_users", schema: "identity");
        }
    }
}
