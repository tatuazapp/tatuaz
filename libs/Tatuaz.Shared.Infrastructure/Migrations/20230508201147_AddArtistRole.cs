using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddArtistRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identity",
                table: "tatuaz_roles",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("92a311a0-4677-4f5c-9e19-88d5a3190041"), "Artist" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "tatuaz_roles",
                keyColumn: "id",
                keyValue: new Guid("92a311a0-4677-4f5c-9e19-88d5a3190041")
            );
        }
    }
}
