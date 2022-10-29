using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Gateway.Infrastructure.Migrations
{
    public partial class TempMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tatuaz_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tatuaz_users",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tatuaz_user_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    tatuaz_user_id = table.Column<string>(type: "text", nullable: false),
                    tatuaz_role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_roles_tatuaz_role_id",
                        column: x => x.tatuaz_role_id,
                        principalTable: "tatuaz_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id",
                        column: x => x.tatuaz_user_id,
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_tatuaz_role_id",
                table: "tatuaz_user_roles",
                column: "tatuaz_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_tatuaz_user_id",
                table: "tatuaz_user_roles",
                column: "tatuaz_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tatuaz_user_roles");

            migrationBuilder.DropTable(
                name: "tatuaz_roles");

            migrationBuilder.DropTable(
                name: "tatuaz_users");
        }
    }
}
