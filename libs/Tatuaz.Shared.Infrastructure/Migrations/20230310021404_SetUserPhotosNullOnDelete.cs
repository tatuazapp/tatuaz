using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetUserPhotosNullOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tatuaz_users_photo_background_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.DropForeignKey(
                name: "fk_tatuaz_users_photo_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.DropIndex(
                name: "ix_tatuaz_users_background_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.DropIndex(
                name: "ix_tatuaz_users_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_background_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "background_photo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "foreground_photo_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_tatuaz_users_photo_background_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "background_photo_id",
                principalSchema: "photo",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_tatuaz_users_photo_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "foreground_photo_id",
                principalSchema: "photo",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tatuaz_users_photo_background_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.DropForeignKey(
                name: "fk_tatuaz_users_photo_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.DropIndex(
                name: "ix_tatuaz_users_background_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.DropIndex(
                name: "ix_tatuaz_users_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users");

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_background_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "background_photo_id");

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "foreground_photo_id");

            migrationBuilder.AddForeignKey(
                name: "fk_tatuaz_users_photo_background_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "background_photo_id",
                principalSchema: "photo",
                principalTable: "photos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_tatuaz_users_photo_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "foreground_photo_id",
                principalSchema: "photo",
                principalTable: "photos",
                principalColumn: "id");
        }
    }
}
