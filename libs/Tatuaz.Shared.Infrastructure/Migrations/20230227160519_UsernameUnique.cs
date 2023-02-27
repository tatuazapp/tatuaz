using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsernameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_username",
                schema: "identity",
                table: "tatuaz_users",
                column: "username",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tatuaz_users_username",
                schema: "identity",
                table: "tatuaz_users"
            );
        }
    }
}
