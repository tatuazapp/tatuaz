using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "post");

            migrationBuilder.CreateTable(
                name: "posts",
                schema: "post",
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
                    table.PrimaryKey("pk_posts", x => x.id);
                    table.ForeignKey(
                        name: "fk_posts_tatuaz_users_author_id",
                        column: x => x.authorid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "comments",
                schema: "post",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_comments", x => x.id);
                    table.ForeignKey(
                        name: "fk_comments_comments_parent_comment_id",
                        column: x => x.parentcommentid,
                        principalSchema: "post",
                        principalTable: "comments",
                        principalColumn: "id"
                    );
                    table.ForeignKey(
                        name: "fk_comments_post_post_id",
                        column: x => x.postid,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_comments_tatuaz_users_user_id",
                        column: x => x.userid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "post_likes",
                schema: "post",
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
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_likes", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_likes_posts_post_id",
                        column: x => x.postid,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_post_likes_tatuaz_users_user_id",
                        column: x => x.userid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "post_photos",
                schema: "post",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        postid = table.Column<Guid>(name: "post_id", type: "uuid", nullable: false),
                        photoid = table.Column<Guid>(
                            name: "photo_id",
                            type: "uuid",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_post_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_post_photos_photos_photo_id",
                        column: x => x.photoid,
                        principalSchema: "photo",
                        principalTable: "photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_post_photos_posts_post_id",
                        column: x => x.postid,
                        principalSchema: "post",
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "comment_likes",
                schema: "post",
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
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_likes", x => x.id);
                    table.ForeignKey(
                        name: "fk_comment_likes_comments_comment_id",
                        column: x => x.commentid,
                        principalSchema: "post",
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_comment_likes_tatuaz_users_user_id",
                        column: x => x.userid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_comment_likes_comment_id",
                schema: "post",
                table: "comment_likes",
                column: "comment_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_comment_likes_user_id",
                schema: "post",
                table: "comment_likes",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_comments_parent_comment_id",
                schema: "post",
                table: "comments",
                column: "parent_comment_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_comments_post_id",
                schema: "post",
                table: "comments",
                column: "post_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_comments_user_id",
                schema: "post",
                table: "comments",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_post_likes_post_id",
                schema: "post",
                table: "post_likes",
                column: "post_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_post_likes_user_id",
                schema: "post",
                table: "post_likes",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_post_photos_photo_id",
                schema: "post",
                table: "post_photos",
                column: "photo_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_post_photos_post_id",
                schema: "post",
                table: "post_photos",
                column: "post_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_posts_author_id",
                schema: "post",
                table: "posts",
                column: "author_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "comment_likes", schema: "post");

            migrationBuilder.DropTable(name: "post_likes", schema: "post");

            migrationBuilder.DropTable(name: "post_photos", schema: "post");

            migrationBuilder.DropTable(name: "comments", schema: "post");

            migrationBuilder.DropTable(name: "posts", schema: "post");
        }
    }
}
