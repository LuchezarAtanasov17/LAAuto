﻿// <auto-generated />
using System;
using LAAuto.Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LAAuto.Entities.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LAAuto.Entities.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9d8afaca-f28c-4fce-bc14-5c3363633323"),
                            CategoryId = new Guid("7294f257-a657-4797-8fce-272319ade2f9"),
                            EndDate = new DateTime(2022, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceId = new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                            StartDate = new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2")
                        });
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7294f257-a657-4797-8fce-272319ade2f9"),
                            Name = "Преглед"
                        },
                        new
                        {
                            Id = new Guid("e5dea772-dfa1-43ab-a89f-0a91df10123b"),
                            Name = "Диагностика"
                        },
                        new
                        {
                            Id = new Guid("0c1237b8-2fe4-43f7-b6dc-2a0a4ef0713d"),
                            Name = "Смяна на масло"
                        });
                });

            modelBuilder.Entity("LAAuto.Entities.Models.CategoryService", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoryId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("CategoryServices");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("7294f257-a657-4797-8fce-272319ade2f9"),
                            ServiceId = new Guid("9226a3f4-35aa-4817-adcd-1c033cf739ad"),
                            Id = new Guid("accfbc90-1486-44b8-9a97-caeecf550391")
                        },
                        new
                        {
                            CategoryId = new Guid("7294f257-a657-4797-8fce-272319ade2f9"),
                            ServiceId = new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                            Id = new Guid("8317f4c5-3f8b-4020-bbcf-2adb5e30639b")
                        },
                        new
                        {
                            CategoryId = new Guid("0c1237b8-2fe4-43f7-b6dc-2a0a4ef0713d"),
                            ServiceId = new Guid("fce201d7-e941-4f41-b3be-0c265798ede9"),
                            Id = new Guid("6a62c3f8-aa54-4857-9599-fcbba31da47d")
                        });
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eecdc117-7fbe-4c46-bbf9-8507b45c0d88"),
                            ServiceId = new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                            UserId = new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                            Value = 4
                        });
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c35c18b-3177-4ad1-8be7-141693a7272f"),
                            ConcurrencyStamp = "68be544a-11c3-493d-b40f-d6fa9b362c52",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = new Guid("b61a261f-5220-4176-9d49-ff18ecbd5b18"),
                            ConcurrencyStamp = "3a197fb9-6842-4b1a-ada3-89c75a45eaba",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("CloseTime")
                        .HasColumnType("time");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<TimeSpan>("OpenTime")
                        .HasColumnType("time");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Services");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e17b327f-eee6-4011-9905-bc8360cd5e66"),
                            CloseTime = new TimeSpan(0, 18, 0, 0, 0),
                            Location = "Гр.София, кв.Надежда, ул.Стамболийски 36",
                            Name = "Carx",
                            OpenTime = new TimeSpan(0, 9, 0, 0, 0),
                            UserId = new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2")
                        },
                        new
                        {
                            Id = new Guid("9226a3f4-35aa-4817-adcd-1c033cf739ad"),
                            CloseTime = new TimeSpan(0, 18, 0, 0, 0),
                            Location = "Гр.Пловдив, кв.Кичука, ул.Македония 12",
                            Name = "Autox",
                            OpenTime = new TimeSpan(0, 8, 0, 0, 0),
                            UserId = new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2")
                        },
                        new
                        {
                            Id = new Guid("fce201d7-e941-4f41-b3be-0c265798ede9"),
                            CloseTime = new TimeSpan(0, 20, 0, 0, 0),
                            Location = "Гр.Varna, кв.Владиславово, ул.Георги Минков 3",
                            Name = "CarKing",
                            OpenTime = new TimeSpan(0, 10, 0, 0, 0),
                            UserId = new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2")
                        });
                });

            modelBuilder.Entity("LAAuto.Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("62448744-4356-44dc-a005-0bfb6ba9e8b2"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2ca21132-91a9-4653-b74d-acc825c3cc1e",
                            Email = "client@mail.com",
                            EmailConfirmed = false,
                            FirstName = "Pesho",
                            LastName = "Peshov",
                            LockoutEnabled = false,
                            NormalizedEmail = "CLIENT@MAIL.COM",
                            NormalizedUserName = "CLIENT",
                            PhoneNumberConfirmed = false,
                            TwoFactorEnabled = false,
                            UserName = "User"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Appointment", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.Category", "Category")
                        .WithMany("Appointments")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAAuto.Entities.Models.Service", "Service")
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAAuto.Entities.Models.User", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LAAuto.Entities.Models.CategoryService", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.Category", "Category")
                        .WithMany("CategoryServices")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAAuto.Entities.Models.Service", "Service")
                        .WithMany("CategoryServices")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Rating", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.Service", "Service")
                        .WithMany("Ratings")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAAuto.Entities.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Service", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.User", "User")
                        .WithMany("Services")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LAAuto.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("LAAuto.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Category", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("CategoryServices");
                });

            modelBuilder.Entity("LAAuto.Entities.Models.Service", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("CategoryServices");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("LAAuto.Entities.Models.User", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Ratings");

                    b.Navigation("Services");
                });
#pragma warning restore 612, 618
        }
    }
}
