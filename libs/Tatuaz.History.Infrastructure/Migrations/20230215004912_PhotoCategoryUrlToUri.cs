using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PhotoCategoryUrlToUri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_url",
                schema: "H_photo",
                table: "H_photo_categories",
                newName: "image_uri"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_uri",
                schema: "H_photo",
                table: "H_photo_categories",
                newName: "image_url"
            );
        }
    }
}
