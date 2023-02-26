using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "photo");

            migrationBuilder.EnsureSchema(name: "general");

            migrationBuilder.EnsureSchema(name: "identity");

            migrationBuilder.AlterDatabase().Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "photo",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        title = table.Column<string>(
                            type: "character varying(64)",
                            maxLength: 64,
                            nullable: false
                        ),
                        type = table.Column<int>(type: "integer", nullable: false),
                        imageuri = table.Column<string>(
                            name: "image_uri",
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "email_info",
                schema: "general",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        recipientemail = table.Column<string>(
                            name: "recipient_email",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        emailtype = table.Column<string>(
                            name: "email_type",
                            type: "character varying(50)",
                            maxLength: 50,
                            nullable: false
                        ),
                        objectid = table.Column<Guid>(
                            name: "object_id",
                            type: "uuid",
                            nullable: false
                        ),
                        orderedat = table.Column<Instant>(
                            name: "ordered_at",
                            type: "timestamp with time zone",
                            nullable: false
                        ),
                        sentat = table.Column<Instant>(
                            name: "sent_at",
                            type: "timestamp with time zone",
                            nullable: true
                        ),
                        retrycount = table.Column<int>(
                            name: "retry_count",
                            type: "integer",
                            nullable: false
                        ),
                        error = table.Column<string>(
                            type: "character varying(500)",
                            maxLength: 500,
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_info", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "photos",
                schema: "photo",
                columns: table =>
                    new
                    {
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
                    table.PrimaryKey("pk_photos", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_roles",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        name = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_roles", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "photo_categories",
                schema: "photo",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        photoid = table.Column<Guid>(
                            name: "photo_id",
                            type: "uuid",
                            nullable: false
                        ),
                        categoryid = table.Column<int>(
                            name: "category_id",
                            type: "integer",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_photo_categories_categories_category_id",
                        column: x => x.categoryid,
                        principalSchema: "photo",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_photo_categories_photo_photo_id",
                        column: x => x.photoid,
                        principalSchema: "photo",
                        principalTable: "photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_users",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<string>(
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        username = table.Column<string>(
                            type: "character varying(32)",
                            maxLength: 32,
                            nullable: false
                        ),
                        auth0id = table.Column<string>(
                            type: "character varying(128)",
                            maxLength: 128,
                            nullable: false
                        ),
                        foregroundphotoid = table.Column<Guid>(
                            name: "foreground_photo_id",
                            type: "uuid",
                            nullable: true
                        ),
                        backgroundphotoid = table.Column<Guid>(
                            name: "background_photo_id",
                            type: "uuid",
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_tatuaz_users_photo_background_photo_id",
                        column: x => x.backgroundphotoid,
                        principalSchema: "photo",
                        principalTable: "photos",
                        principalColumn: "id"
                    );
                    table.ForeignKey(
                        name: "fk_tatuaz_users_photo_foreground_photo_id",
                        column: x => x.foregroundphotoid,
                        principalSchema: "photo",
                        principalTable: "photos",
                        principalColumn: "id"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "tatuaz_user_roles",
                schema: "identity",
                columns: table =>
                    new
                    {
                        id = table.Column<Guid>(type: "uuid", nullable: false),
                        useremail = table.Column<string>(
                            name: "user_email",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        roleid = table.Column<Guid>(name: "role_id", type: "uuid", nullable: false)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tatuaz_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_roles_tatuaz_role_id",
                        column: x => x.roleid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id",
                        column: x => x.useremail,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_categories",
                schema: "photo",
                columns: table =>
                    new
                    {
                        id = table
                            .Column<int>(type: "integer", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        userid = table.Column<string>(
                            name: "user_id",
                            type: "character varying(320)",
                            maxLength: 320,
                            nullable: false
                        ),
                        categoryid = table.Column<int>(
                            name: "category_id",
                            type: "integer",
                            nullable: false
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_categories", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_categories_categories_category_id",
                        column: x => x.categoryid,
                        principalSchema: "photo",
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_user_categories_tatuaz_users_user_id",
                        column: x => x.userid,
                        principalSchema: "identity",
                        principalTable: "tatuaz_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_email_info_recipient_email",
                schema: "general",
                table: "email_info",
                column: "recipient_email"
            );

            migrationBuilder.CreateIndex(
                name: "ix_photo_categories_category_id",
                schema: "photo",
                table: "photo_categories",
                column: "category_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_photo_categories_photo_id",
                schema: "photo",
                table: "photo_categories",
                column: "photo_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_role_id",
                schema: "identity",
                table: "tatuaz_user_roles",
                column: "role_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_user_roles_user_email",
                schema: "identity",
                table: "tatuaz_user_roles",
                column: "user_email"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_background_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "background_photo_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_tatuaz_users_foreground_photo_id",
                schema: "identity",
                table: "tatuaz_users",
                column: "foreground_photo_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_categories_category_id",
                schema: "photo",
                table: "user_categories",
                column: "category_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_categories_user_id",
                schema: "photo",
                table: "user_categories",
                column: "user_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "email_info", schema: "general");

            migrationBuilder.DropTable(name: "photo_categories", schema: "photo");

            migrationBuilder.DropTable(name: "tatuaz_user_roles", schema: "identity");

            migrationBuilder.DropTable(name: "user_categories", schema: "photo");

            migrationBuilder.DropTable(name: "tatuaz_roles", schema: "identity");

            migrationBuilder.DropTable(name: "categories", schema: "photo");

            migrationBuilder.DropTable(name: "tatuaz_users", schema: "identity");

            migrationBuilder.DropTable(name: "photos", schema: "photo");
        }
    }
}
