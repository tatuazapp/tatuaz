using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "popularity",
                schema: "H_photo",
                table: "H_categories"
            );

            migrationBuilder.EnsureSchema(name: "H_post");

            migrationBuilder.CreateTable(
                name: "H_comment_likes",
                schema: "H_post",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        commentid = table.Column<Guid>(
                            name: "comment_id",
                            type: "uuid",
                            nullable: false
                        ),
                        userid = table.Column<string>(
                            name: "user_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_comment_likes", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_comments",
                schema: "H_post",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        parentcommentid = table.Column<Guid>(
                            name: "parent_comment_id",
                            type: "uuid",
                            nullable: true
                        ),
                        postid = table.Column<Guid>(name: "post_id", type: "uuid", nullable: false),
                        userid = table.Column<string>(
                            name: "user_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        content = table.Column<string>(
                            type: "character varying(1024)",
                            maxLength: 1024,
                            nullable: false
                        ),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        modifiedby = table.Column<string>(
                            name: "modified_by",
                            type: "text",
                            nullable: false
                        ),
                        modifiedat = table.Column<Instant>(
                            name: "modified_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        createdby = table.Column<string>(
                            name: "created_by",
                            type: "text",
                            nullable: false
                        ),
                        createdat = table.Column<Instant>(
                            name: "created_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_comments", x => x.histid);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_post_likes",
                schema: "H_post",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        postid = table.Column<Guid>(name: "post_id", type: "uuid", nullable: false),
                        userid = table.Column<string>(
                            name: "user_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_post_likes", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_post_photos",
                schema: "H_post",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        postid = table.Column<Guid>(name: "post_id", type: "uuid", nullable: false),
                        photoid = table.Column<Guid>(
                            name: "photo_id",
                            type: "uuid",
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_post_photos", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "H_posts",
                schema: "H_post",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        authorid = table.Column<string>(
                            name: "author_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        description = table.Column<string>(
                            type: "character varying(4096)",
                            maxLength: 4096,
                            nullable: false
                        ),
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
                        histstate = table.Column<int>(
                            name: "hist_state",
                            type: "integer",
                            nullable: false
                        ),
                        histdumpedat = table.Column<Instant>(
                            name: "hist_dumped_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        modifiedby = table.Column<string>(
                            name: "modified_by",
                            type: "text",
                            nullable: false
                        ),
                        modifiedat = table.Column<Instant>(
                            name: "modified_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        createdby = table.Column<string>(
                            name: "created_by",
                            type: "text",
                            nullable: false
                        ),
                        createdat = table.Column<Instant>(
                            name: "created_at",
                            type: "timestamp with time zone",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_posts", x => x.id);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "H_comment_likes", schema: "H_post");

            migrationBuilder.DropTable(name: "H_comments", schema: "H_post");

            migrationBuilder.DropTable(name: "H_post_likes", schema: "H_post");

            migrationBuilder.DropTable(name: "H_post_photos", schema: "H_post");

            migrationBuilder.DropTable(name: "H_posts", schema: "H_post");

            migrationBuilder.AddColumn<int>(
                name: "popularity",
                schema: "H_photo",
                table: "H_categories",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );
        }
    }
}
