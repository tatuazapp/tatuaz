using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPhotoCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_photo_categories",
                schema: "photo",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        userid = table.Column<string>(
                            name: "user_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        photocategoryid = table.Column<int>(
                            name: "photo_category_id",
                            type: "integer",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_photo_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_photo_categories_photo_categories_photo_category_id",
                        column: x => x.photocategoryid,
                        principalSchema: "photo",
                        principalTable: "photo_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_user_photo_categories_tatuaz_users_user_id",
                        column: x => x.userid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_photo_categories_photo_category_id",
                schema: "photo",
                table: "user_photo_categories",
                column: "photo_category_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_photo_categories_user_id",
                schema: "photo",
                table: "user_photo_categories",
                column: "user_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "user_photo_categories", schema: "photo");
        }
    }
}
