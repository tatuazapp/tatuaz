using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPopularity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "identity",
                table: "tatuaz_users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "popularity",
                schema: "identity",
                table: "tatuaz_users",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "city", schema: "identity", table: "tatuaz_users");

            migrationBuilder.DropColumn(
                name: "popularity",
                schema: "identity",
                table: "tatuaz_users"
            );
        }
    }
}
