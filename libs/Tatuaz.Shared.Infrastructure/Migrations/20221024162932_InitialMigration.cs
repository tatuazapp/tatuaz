#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tatuaz.Shared.Infrastructure.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "identity");

        migrationBuilder.CreateTable(
            name: "tatuaz_roles",
            schema: "identity",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_roles", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "tatuaz_users",
            schema: "identity",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                normalized_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                email_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                password_hash = table.Column<string>(type: "text", nullable: true),
                security_stamp = table.Column<string>(type: "text", nullable: true),
                concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                phone_number = table.Column<string>(type: "text", nullable: true),
                phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false),
                two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                lockout_enabled = table.Column<bool>(type: "boolean", nullable: false),
                access_failed_count = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "tatuaz_role_claims",
            schema: "identity",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                role_id = table.Column<Guid>(type: "uuid", nullable: false),
                claim_type = table.Column<string>(type: "text", nullable: true),
                claim_value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_role_claims", x => x.id);
                table.ForeignKey(
                    name: "fk_tatuaz_role_claims_tatuaz_roles_role_id",
                    column: x => x.role_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "tatuaz_user_claims",
            schema: "identity",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                claim_type = table.Column<string>(type: "text", nullable: true),
                claim_value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_user_claims", x => x.id);
                table.ForeignKey(
                    name: "fk_tatuaz_user_claims_tatuaz_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "tatuaz_user_logins",
            schema: "identity",
            columns: table => new
            {
                login_provider = table.Column<string>(type: "text", nullable: false),
                provider_key = table.Column<string>(type: "text", nullable: false),
                provider_display_name = table.Column<string>(type: "text", nullable: true),
                user_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_user_logins", x => new { x.login_provider, x.provider_key });
                table.ForeignKey(
                    name: "fk_tatuaz_user_logins_tatuaz_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "tatuaz_user_roles",
            schema: "identity",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                role_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_user_roles", x => new { x.user_id, x.role_id });
                table.ForeignKey(
                    name: "fk_tatuaz_user_roles_tatuaz_roles_role_id",
                    column: x => x.role_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_tatuaz_user_roles_tatuaz_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "tatuaz_user_tokens",
            schema: "identity",
            columns: table => new
            {
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                login_provider = table.Column<string>(type: "text", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tatuaz_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                table.ForeignKey(
                    name: "fk_tatuaz_user_tokens_tatuaz_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "identity",
                    principalTable: "tatuaz_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_tatuaz_role_claims_role_id",
            schema: "identity",
            table: "tatuaz_role_claims",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: "identity",
            table: "tatuaz_roles",
            column: "normalized_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_tatuaz_user_claims_user_id",
            schema: "identity",
            table: "tatuaz_user_claims",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_tatuaz_user_logins_user_id",
            schema: "identity",
            table: "tatuaz_user_logins",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "ix_tatuaz_user_roles_role_id",
            schema: "identity",
            table: "tatuaz_user_roles",
            column: "role_id");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: "identity",
            table: "tatuaz_users",
            column: "normalized_email");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: "identity",
            table: "tatuaz_users",
            column: "normalized_user_name",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "tatuaz_role_claims",
            schema: "identity");

        migrationBuilder.DropTable(
            name: "tatuaz_user_claims",
            schema: "identity");

        migrationBuilder.DropTable(
            name: "tatuaz_user_logins",
            schema: "identity");

        migrationBuilder.DropTable(
            name: "tatuaz_user_roles",
            schema: "identity");

        migrationBuilder.DropTable(
            name: "tatuaz_user_tokens",
            schema: "identity");

        migrationBuilder.DropTable(
            name: "tatuaz_roles",
            schema: "identity");

        migrationBuilder.DropTable(
            name: "tatuaz_users",
            schema: "identity");
    }
}
