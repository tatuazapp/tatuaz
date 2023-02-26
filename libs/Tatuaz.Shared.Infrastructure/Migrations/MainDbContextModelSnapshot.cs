﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tatuaz.Shared.Infrastructure.DataAccess;

#nullable disable

namespace Tatuaz.Shared.Infrastructure.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.General.EmailInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("EmailType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email_type");

                    b.Property<string>("Error")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("error");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid")
                        .HasColumnName("object_id");

                    b.Property<Instant>("OrderedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ordered_at");

                    b.Property<string>("RecipientEmail")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)")
                        .HasColumnName("recipient_email");

                    b.Property<int>("RetryCount")
                        .HasColumnType("integer")
                        .HasColumnName("retry_count");

                    b.Property<Instant?>("SentAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sent_at");

                    b.HasKey("Id")
                        .HasName("pk_email_info");

                    b.HasIndex("RecipientEmail")
                        .HasDatabaseName("ix_email_info_recipient_email");

                    b.ToTable("email_info", "general");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_tatuaz_roles");

                    b.ToTable("tatuaz_roles", "identity");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUser", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)")
                        .HasColumnName("id");

                    b.Property<string>("Auth0Id")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("auth0id");

                    b.Property<Guid?>("BackgroundPhotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("background_photo_id");

                    b.Property<Guid?>("ForegroundPhotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("foreground_photo_id");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_tatuaz_users");

                    b.HasIndex("BackgroundPhotoId")
                        .HasDatabaseName("ix_tatuaz_users_background_photo_id");

                    b.HasIndex("ForegroundPhotoId")
                        .HasDatabaseName("ix_tatuaz_users_foreground_photo_id");

                    b.ToTable("tatuaz_users", "identity");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)")
                        .HasColumnName("user_email");

                    b.HasKey("Id")
                        .HasName("pk_tatuaz_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_tatuaz_user_roles_role_id");

                    b.HasIndex("UserEmail")
                        .HasDatabaseName("ix_tatuaz_user_roles_user_email");

                    b.ToTable("tatuaz_user_roles", "identity");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("image_uri");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("title");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", "photo");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Instant>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("created_by");

                    b.Property<Instant>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("modified_at");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("modified_by");

                    b.HasKey("Id")
                        .HasName("pk_photos");

                    b.ToTable("photos", "photo");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.PhotoCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uuid")
                        .HasColumnName("photo_id");

                    b.HasKey("Id")
                        .HasName("pk_photo_categories");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_photo_categories_category_id");

                    b.HasIndex("PhotoId")
                        .HasDatabaseName("ix_photo_categories_photo_id");

                    b.ToTable("photo_categories", "photo");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.UserCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("category_id");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_categories");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_user_categories_category_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_categories_user_id");

                    b.ToTable("user_categories", "photo");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUser", b =>
                {
                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Photo.Photo", "BackgroundPhoto")
                        .WithMany()
                        .HasForeignKey("BackgroundPhotoId")
                        .HasConstraintName("fk_tatuaz_users_photo_background_photo_id");

                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Photo.Photo", "ForegroundPhoto")
                        .WithMany()
                        .HasForeignKey("ForegroundPhotoId")
                        .HasConstraintName("fk_tatuaz_users_photo_foreground_photo_id");

                    b.Navigation("BackgroundPhoto");

                    b.Navigation("ForegroundPhoto");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUserRole", b =>
                {
                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazRole", "Role")
                        .WithMany("TatuazUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tatuaz_user_roles_tatuaz_roles_tatuaz_role_id");

                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tatuaz_user_roles_tatuaz_users_tatuaz_user_id");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.PhotoCategory", b =>
                {
                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Photo.Category", "Category")
                        .WithMany("PhotoCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_photo_categories_categories_category_id");

                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Photo.Photo", "Photo")
                        .WithMany("PhotoCategories")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_photo_categories_photo_photo_id");

                    b.Navigation("Category");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.UserCategory", b =>
                {
                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Photo.Category", "Category")
                        .WithMany("UserCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_categories_categories_category_id");

                    b.HasOne("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUser", "User")
                        .WithMany("UserPhotoCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_categories_tatuaz_users_user_id");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazRole", b =>
                {
                    b.Navigation("TatuazUserRoles");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Identity.TatuazUser", b =>
                {
                    b.Navigation("UserPhotoCategories");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.Category", b =>
                {
                    b.Navigation("PhotoCategories");

                    b.Navigation("UserCategories");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Models.Photo.Photo", b =>
                {
                    b.Navigation("PhotoCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
