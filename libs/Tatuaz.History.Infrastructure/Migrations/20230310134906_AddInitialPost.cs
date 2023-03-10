using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_photo",
                table: "H_user_categories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(320)",
                oldMaxLength: 320);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_post",
                table: "H_post_likes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(320)",
                oldMaxLength: 320);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_post",
                table: "H_comments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(320)",
                oldMaxLength: 320);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_post",
                table: "H_comment_likes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(320)",
                oldMaxLength: 320);

            migrationBuilder.CreateTable(
                name: "H_initial_post_photos",
                schema: "H_post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    initialpostid = table.Column<Guid>(name: "initial_post_id", type: "uuid", nullable: false),
                    photoid = table.Column<Guid>(name: "photo_id", type: "uuid", nullable: false),
                    histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                    histstate = table.Column<int>(name: "hist_state", type: "integer", nullable: false),
                    histdumpedat = table.Column<Instant>(name: "hist_dumped_at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_initial_post_photos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "H_initial_posts",
                schema: "H_post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                    histstate = table.Column<int>(name: "hist_state", type: "integer", nullable: false),
                    histdumpedat = table.Column<Instant>(name: "hist_dumped_at", type: "timestamp with time zone", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "text", nullable: false),
                    modifiedat = table.Column<Instant>(name: "modified_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: false),
                    createdat = table.Column<Instant>(name: "created_at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_initial_posts", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "H_initial_post_photos",
                schema: "H_post");

            migrationBuilder.DropTable(
                name: "H_initial_posts",
                schema: "H_post");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_photo",
                table: "H_user_categories",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_post",
                table: "H_post_likes",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_post",
                table: "H_comments",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "H_post",
                table: "H_comment_likes",
                type: "character varying(320)",
                maxLength: 320,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
