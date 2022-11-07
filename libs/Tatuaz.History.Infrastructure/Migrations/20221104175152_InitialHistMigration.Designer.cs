// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tatuaz.History.DataAccess;
using Tatuaz.Shared.Domain.Entities.Hist.Models.Common;

#nullable disable

namespace Tatuaz.History.DataAccess.Migrations
{
    [DbContext(typeof(HistDbContext))]
    [Migration("20221104175152_InitialHistMigration")]
    partial class InitialHistMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "hist_state", new[] { "added", "modified", "deleted" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Hist.Models.Identity.HistTatuazRole", b =>
                {
                    b.Property<Guid>("HistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("hist_id");

                    b.Property<Instant>("HistDumpedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("hist_dumped_at");

                    b.Property<HistState>("HistState")
                        .HasColumnType("hist_state")
                        .HasColumnName("hist_state");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("HistId")
                        .HasName("pk_h_tatuaz_roles");

                    b.ToTable("H_tatuaz_roles", "H_Identity");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Hist.Models.Identity.HistTatuazUser", b =>
                {
                    b.Property<Guid>("HistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("hist_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<Instant>("HistDumpedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("hist_dumped_at");

                    b.Property<HistState>("HistState")
                        .HasColumnType("hist_state")
                        .HasColumnName("hist_state");

                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("username");

                    b.HasKey("HistId")
                        .HasName("pk_h_tatuaz_users");

                    b.ToTable("H_tatuaz_users", "H_Identity");
                });

            modelBuilder.Entity("Tatuaz.Shared.Domain.Entities.Hist.Models.Identity.HistTatuazUserRole", b =>
                {
                    b.Property<Guid>("HistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("hist_id");

                    b.Property<Instant>("HistDumpedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("hist_dumped_at");

                    b.Property<HistState>("HistState")
                        .HasColumnType("hist_state")
                        .HasColumnName("hist_state");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("TatuazRoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("tatuaz_role_id");

                    b.Property<string>("TatuazUserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("tatuaz_user_id");

                    b.HasKey("HistId")
                        .HasName("pk_h_tatuaz_user_roles");

                    b.ToTable("H_tatuaz_user_roles", "H_Identity");
                });
#pragma warning restore 612, 618
        }
    }
}
