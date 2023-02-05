using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPhotoCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "H_user_photo_categories",
                schema: "H_photo",
                columns: table =>
                    new
                    {
                        histid = table.Column<Guid>(name: "hist_id", type: "uuid", nullable: false),
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
                        id = table.Column<Guid>(type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_h_user_photo_categories", x => x.histid);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "H_user_photo_categories", schema: "H_photo");
        }
    }
}
