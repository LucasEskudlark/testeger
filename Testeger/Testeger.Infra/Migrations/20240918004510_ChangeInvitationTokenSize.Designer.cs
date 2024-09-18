﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testeger.Infra.Context;

#nullable disable

namespace Testeger.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240918004510_ChangeInvitationTokenSize")]
    partial class ChangeInvitationTokenSize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Image", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("varchar(2048)");

                    b.Property<string>("TestCaseResultId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("TestCaseResultId");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Invitation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Email");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpiryDate");

                    b.Property<string>("InvitationToken")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)")
                        .HasColumnName("InvitationToken");

                    b.Property<ulong>("IsConfirmed")
                        .HasColumnType("bit")
                        .HasColumnName("IsConfirmed");

                    b.Property<string>("ProjectId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("ProjectId");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("SentDate");

                    b.Property<string>("UserId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("InvitationToken")
                        .IsUnique();

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Invitations", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValue(new DateTime(2024, 9, 18, 0, 45, 10, 490, DateTimeKind.Utc).AddTicks(2465))
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.ProjectUser", b =>
                {
                    b.Property<string>("ProjectId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestCase", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CompletedDate");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValue(new DateTime(2024, 9, 18, 0, 45, 10, 493, DateTimeKind.Utc).AddTicks(3307))
                        .HasColumnName("CreatedDate");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("ProjectId");

                    b.Property<DateTime?>("ScheduledDate")
                        .HasColumnType("datetime")
                        .HasColumnName("ScheduledDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Status");

                    b.Property<string>("TestRequestId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("TestRequestId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("TestRequestId");

                    b.ToTable("TestCase", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestCaseResult", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ActualResult")
                        .HasMaxLength(700)
                        .HasColumnType("varchar(700)")
                        .HasColumnName("ActualResult");

                    b.Property<TimeSpan?>("ElapsedTime")
                        .HasColumnType("time")
                        .HasColumnName("ElapsedTime");

                    b.Property<ulong?>("IsFinished")
                        .HasColumnType("bit")
                        .HasColumnName("IsFinished");

                    b.Property<ulong?>("IsSuccess")
                        .HasColumnType("bit")
                        .HasColumnName("IsSuccess");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("Number");

                    b.Property<string>("TestCaseId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("TestCaseId");

                    b.HasKey("Id");

                    b.HasIndex("TestCaseId");

                    b.ToTable("TestCaseResult", (string)null);
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("CompletedDate");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValue(new DateTime(2024, 9, 18, 0, 45, 10, 491, DateTimeKind.Utc).AddTicks(1602))
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("varchar(1500)")
                        .HasColumnName("Description");

                    b.Property<DateTime?>("DueDate")
                        .IsRequired()
                        .HasColumnType("datetime")
                        .HasColumnName("DueDate");

                    b.Property<int>("Number")
                        .HasColumnType("int")
                        .HasColumnName("Number");

                    b.Property<string>("PriorityLevel")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PriorityLevel");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("ProjectId");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Status");

                    b.Property<string>("StoryLink")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)")
                        .HasColumnName("StoryLink");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Title");

                    b.Property<string>("UserAssignedId")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)")
                        .HasColumnName("UserAssignedId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("TestRequest", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testeger.Domain.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Image", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.TestCaseResult", "TestCaseResult")
                        .WithMany("Images")
                        .HasForeignKey("TestCaseResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestCaseResult");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Invitation", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.Project", "Project")
                        .WithMany("Invitations")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Testeger.Domain.Models.Entities.ApplicationUser", "User")
                        .WithMany("Invitations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.ProjectUser", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testeger.Domain.Models.Entities.ApplicationUser", "User")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestCase", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.TestRequest", "TestRequest")
                        .WithMany("TestCases")
                        .HasForeignKey("TestRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Testeger.Domain.Models.ValueObjects.TestCaseDetails", "Details", b1 =>
                        {
                            b1.Property<string>("TestCaseId")
                                .HasColumnType("varchar(255)");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(1500)
                                .HasColumnType("varchar(1500)")
                                .HasColumnName("Description");

                            b1.Property<string>("Environment")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)")
                                .HasColumnName("Environment");

                            b1.Property<string>("ExpectedResult")
                                .IsRequired()
                                .HasMaxLength(700)
                                .HasColumnType("varchar(700)")
                                .HasColumnName("ExpectedResult");

                            b1.Property<string>("PreConditions")
                                .IsRequired()
                                .HasMaxLength(700)
                                .HasColumnType("varchar(700)")
                                .HasColumnName("PreConditions");

                            b1.Property<string>("Steps")
                                .IsRequired()
                                .HasMaxLength(700)
                                .HasColumnType("varchar(700)")
                                .HasColumnName("Steps");

                            b1.HasKey("TestCaseId");

                            b1.ToTable("TestCase");

                            b1.WithOwner()
                                .HasForeignKey("TestCaseId");
                        });

                    b.OwnsMany("Testeger.Domain.Models.ValueObjects.TestCaseHistory", "History", b1 =>
                        {
                            b1.Property<string>("TestCaseId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("ChangedByUserId")
                                .IsRequired()
                                .HasMaxLength(36)
                                .HasColumnType("varchar(36)")
                                .HasColumnName("ChangedByUserId");

                            b1.Property<DateTime>("ChangedDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime")
                                .HasDefaultValue(new DateTime(2024, 9, 18, 0, 45, 10, 497, DateTimeKind.Utc).AddTicks(813))
                                .HasColumnName("ChangedDate");

                            b1.Property<string>("NewStatus")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("NewStatus");

                            b1.Property<string>("OldStatus")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("OldStatus");

                            b1.HasKey("TestCaseId", "Id");

                            b1.ToTable("TestCaseHistory", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TestCaseId");
                        });

                    b.Navigation("Details")
                        .IsRequired();

                    b.Navigation("History");

                    b.Navigation("TestRequest");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestCaseResult", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.TestCase", "TestCase")
                        .WithMany("Results")
                        .HasForeignKey("TestCaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestCase");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestRequest", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.Project", "Project")
                        .WithMany("TestRequests")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Testeger.Domain.Models.ValueObjects.TestRequestHistory", "History", b1 =>
                        {
                            b1.Property<string>("TestRequestId")
                                .HasColumnType("varchar(255)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("ChangedByUserId")
                                .IsRequired()
                                .HasMaxLength(36)
                                .HasColumnType("varchar(36)")
                                .HasColumnName("ChangedByUserId");

                            b1.Property<DateTime>("ChangedDate")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("datetime")
                                .HasDefaultValue(new DateTime(2024, 9, 18, 0, 45, 10, 492, DateTimeKind.Utc).AddTicks(7578))
                                .HasColumnName("ChangedDate");

                            b1.Property<string>("NewStatus")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("NewStatus");

                            b1.Property<string>("OldStatus")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("OldStatus");

                            b1.HasKey("TestRequestId", "Id");

                            b1.ToTable("TestRequestHistory", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TestRequestId");
                        });

                    b.Navigation("History");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("ProjectUsers");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Project", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("ProjectUsers");

                    b.Navigation("TestRequests");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestCase", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestCaseResult", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Testeger.Domain.Models.Entities.TestRequest", b =>
                {
                    b.Navigation("TestCases");
                });
#pragma warning restore 612, 618
        }
    }
}
