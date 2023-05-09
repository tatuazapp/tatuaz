using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPopularity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "H_identity",
                table: "H_tatuaz_users",
                type: "text",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "popularity",
                schema: "H_identity",
                table: "H_tatuaz_users",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "city",
                schema: "H_identity",
                table: "H_tatuaz_users"
            );

            migrationBuilder.DropColumn(
                name: "popularity",
                schema: "H_identity",
                table: "H_tatuaz_users"
            );
        }
    }
}
