using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comments_post_post_id",
                schema: "post",
                table: "comments"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_photo_categories_photo_photo_id",
                schema: "photo",
                table: "photo_categories"
            );

            migrationBuilder.AddColumn<string>(
                name: "bio",
                schema: "identity",
                table: "tatuaz_users",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true
            );

            migrationBuilder.AddForeignKey(
                name: "fk_comments_posts_post_id",
                schema: "post",
                table: "comments",
                column: "post_id",
                principalSchema: "post",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_photo_categories_photos_photo_id",
                schema: "photo",
                table: "photo_categories",
                column: "photo_id",
                principalSchema: "photo",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_comments_posts_post_id",
                schema: "post",
                table: "comments"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_photo_categories_photos_photo_id",
                schema: "photo",
                table: "photo_categories"
            );

            migrationBuilder.DropColumn(name: "bio", schema: "identity", table: "tatuaz_users");

            migrationBuilder.AddForeignKey(
                name: "fk_comments_post_post_id",
                schema: "post",
                table: "comments",
                column: "post_id",
                principalSchema: "post",
                principalTable: "posts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_photo_categories_photo_photo_id",
                schema: "photo",
                table: "photo_categories",
                column: "photo_id",
                principalSchema: "photo",
                principalTable: "photos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
