using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bio",
                schema: "H_identity",
                table: "H_tatuaz_users",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "bio", schema: "H_identity", table: "H_tatuaz_users");
        }
    }
}
