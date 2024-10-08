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
    [Migration("20240812173638_RemoveTestCaseIdFromHistory")]
    partial class RemoveTestCaseIdFromHistory
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

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
                        .HasDefaultValue(new DateTime(2024, 8, 12, 17, 36, 38, 12, DateTimeKind.Utc).AddTicks(936))
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Project", (string)null);
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
                        .HasDefaultValue(new DateTime(2024, 8, 12, 17, 36, 38, 14, DateTimeKind.Utc).AddTicks(2059))
                        .HasColumnName("CreatedDate");

                    b.Property<ulong>("NeedCorrection")
                        .HasColumnType("bit")
                        .HasColumnName("NeedCorrection");

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

                    b.Property<TimeSpan?>("AmountOfTimeSpentToTest")
                        .HasColumnType("time")
                        .HasColumnName("AmountOfTimeSpentToTest");

                    b.Property<TimeSpan?>("ElapsedTime")
                        .HasColumnType("time")
                        .HasColumnName("ElapsedTime");

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
                        .HasDefaultValue(new DateTime(2024, 8, 12, 17, 36, 38, 12, DateTimeKind.Utc).AddTicks(5443))
                        .HasColumnName("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
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

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Image", b =>
                {
                    b.HasOne("Testeger.Domain.Models.Entities.TestCaseResult", "TestCaseResult")
                        .WithMany("Images")
                        .HasForeignKey("TestCaseResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestCaseResult");
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
                                .HasDefaultValue(new DateTime(2024, 8, 12, 17, 36, 38, 18, DateTimeKind.Utc).AddTicks(5488))
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
                                .HasDefaultValue(new DateTime(2024, 8, 12, 17, 36, 38, 13, DateTimeKind.Utc).AddTicks(6994))
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

            modelBuilder.Entity("Testeger.Domain.Models.Entities.Project", b =>
                {
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
