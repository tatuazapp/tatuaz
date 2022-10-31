using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "H_Identity");

        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:hist_state", "added,modified,deleted");

        migrationBuilder.CreateTable(
            name: "H_tatuaz_roles",
            schema: "H_Identity",
            columns: table => new
            {
                hist_id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                hist_state = table.Column<HistState>(type: "hist_state", nullable: false),
                hist_dumped_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_h_tatuaz_roles", x => x.hist_id);
            });

        migrationBuilder.CreateTable(
            name: "H_tatuaz_user_roles",
            schema: "H_Identity",
            columns: table => new
            {
                hist_id = table.Column<Guid>(type: "uuid", nullable: false),
                tatuaz_user_id = table.Column<string>(type: "text", nullable: false),
                tatuaz_role_id = table.Column<Guid>(type: "uuid", nullable: false),
                hist_state = table.Column<HistState>(type: "hist_state", nullable: false),
                hist_dumped_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_h_tatuaz_user_roles", x => x.hist_id);
            });

        migrationBuilder.CreateTable(
            name: "H_tatuaz_users",
            schema: "H_Identity",
            columns: table => new
            {
                hist_id = table.Column<Guid>(type: "uuid", nullable: false),
                username = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                phone_number = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                hist_state = table.Column<HistState>(type: "hist_state", nullable: false),
                hist_dumped_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                id = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_h_tatuaz_users", x => x.hist_id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "H_tatuaz_roles",
            schema: "H_Identity");

        migrationBuilder.DropTable(
            name: "H_tatuaz_user_roles",
            schema: "H_Identity");

        migrationBuilder.DropTable(
            name: "H_tatuaz_users",
            schema: "H_Identity");
    }
}
