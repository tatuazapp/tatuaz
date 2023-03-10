using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "initial_posts",
                schema: "post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "text", nullable: false),
                    modifiedat = table.Column<Instant>(name: "modified_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "text", nullable: false),
                    createdat = table.Column<Instant>(name: "created_at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_initial_posts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "initial_post_photos",
                schema: "post",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    initialpostid = table.Column<Guid>(name: "initial_post_id", type: "uuid", nullable: false),
                    photoid = table.Column<Guid>(name: "photo_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_initial_post_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_initial_post_photos_initial_posts_initial_post_id",
                        column: x => x.initialpostid,
                        principalSchema: "post",
                        principalTable: "initial_posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_initial_post_photos_photos_photo_id",
                        column: x => x.photoid,
                        principalSchema: "photo",
                        principalTable: "photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_initial_post_photos_initial_post_id",
                schema: "post",
                table: "initial_post_photos",
                column: "initial_post_id");

            migrationBuilder.CreateIndex(
                name: "ix_initial_post_photos_photo_id",
                schema: "post",
                table: "initial_post_photos",
                column: "photo_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "initial_post_photos",
                schema: "post");

            migrationBuilder.DropTable(
                name: "initial_posts",
                schema: "post");
        }
    }
}
