using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations;

public partial class InitialMainMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "identity");

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
                    phone_number = table.Column<string>(
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
            name: "tatuaz_user_roles",
            schema: "identity",
            columns: table =>
                new
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
                    principalSchema: "identity",
                    principalTable: "tatuaz_roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id",
                    column: x => x.tatuaz_user_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
            }
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
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "tatuaz_user_roles", schema: "identity");

        migrationBuilder.DropTable(name: "tatuaz_roles", schema: "identity");

        migrationBuilder.DropTable(name: "tatuaz_users", schema: "identity");
    }
}
