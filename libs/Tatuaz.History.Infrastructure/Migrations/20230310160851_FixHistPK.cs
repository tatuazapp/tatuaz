using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixHistPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "pk_h_posts", schema: "H_post", table: "H_posts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_post_photos",
                schema: "H_post",
                table: "H_post_photos"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_post_likes",
                schema: "H_post",
                table: "H_post_likes"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_photos",
                schema: "H_photo",
                table: "H_photos"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_photo_categories",
                schema: "H_photo",
                table: "H_photo_categories"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_initial_posts",
                schema: "H_post",
                table: "H_initial_posts"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_initial_post_photos",
                schema: "H_post",
                table: "H_initial_post_photos"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_comment_likes",
                schema: "H_post",
                table: "H_comment_likes"
            );

            migrationBuilder
                .AlterColumn<int>(
                    name: "id",
                    schema: "H_photo",
                    table: "H_photo_categories",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer"
                )
                .OldAnnotation(
                    "Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_posts",
                schema: "H_post",
                table: "H_posts",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_post_photos",
                schema: "H_post",
                table: "H_post_photos",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_post_likes",
                schema: "H_post",
                table: "H_post_likes",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_photos",
                schema: "H_photo",
                table: "H_photos",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_photo_categories",
                schema: "H_photo",
                table: "H_photo_categories",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_initial_posts",
                schema: "H_post",
                table: "H_initial_posts",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_initial_post_photos",
                schema: "H_post",
                table: "H_initial_post_photos",
                column: "hist_id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_comment_likes",
                schema: "H_post",
                table: "H_comment_likes",
                column: "hist_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "pk_h_posts", schema: "H_post", table: "H_posts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_post_photos",
                schema: "H_post",
                table: "H_post_photos"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_post_likes",
                schema: "H_post",
                table: "H_post_likes"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_photos",
                schema: "H_photo",
                table: "H_photos"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_photo_categories",
                schema: "H_photo",
                table: "H_photo_categories"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_initial_posts",
                schema: "H_post",
                table: "H_initial_posts"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_initial_post_photos",
                schema: "H_post",
                table: "H_initial_post_photos"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_h_comment_likes",
                schema: "H_post",
                table: "H_comment_likes"
            );

            migrationBuilder
                .AlterColumn<int>(
                    name: "id",
                    schema: "H_photo",
                    table: "H_photo_categories",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer"
                )
                .Annotation(
                    "Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_posts",
                schema: "H_post",
                table: "H_posts",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_post_photos",
                schema: "H_post",
                table: "H_post_photos",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_post_likes",
                schema: "H_post",
                table: "H_post_likes",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_photos",
                schema: "H_photo",
                table: "H_photos",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_photo_categories",
                schema: "H_photo",
                table: "H_photo_categories",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_initial_posts",
                schema: "H_post",
                table: "H_initial_posts",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_initial_post_photos",
                schema: "H_post",
                table: "H_initial_post_photos",
                column: "id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_h_comment_likes",
                schema: "H_post",
                table: "H_comment_likes",
                column: "id"
            );
        }
    }
}
