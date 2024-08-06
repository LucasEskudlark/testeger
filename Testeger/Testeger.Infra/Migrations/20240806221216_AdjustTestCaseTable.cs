using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTestCaseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 111, DateTimeKind.Utc).AddTicks(8300),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 778, DateTimeKind.Utc).AddTicks(1027));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 104, DateTimeKind.Utc).AddTicks(2532),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 773, DateTimeKind.Utc).AddTicks(7658));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 116, DateTimeKind.Utc).AddTicks(1183),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 794, DateTimeKind.Utc).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "TestCase",
                keyColumn: "Steps",
                keyValue: null,
                column: "Steps",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Steps",
                table: "TestCase",
                type: "varchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledDate",
                table: "TestCase",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.UpdateData(
                table: "TestCase",
                keyColumn: "PreConditions",
                keyValue: null,
                column: "PreConditions",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PreConditions",
                table: "TestCase",
                type: "varchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "TestCase",
                keyColumn: "ExpectedResult",
                keyValue: null,
                column: "ExpectedResult",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedResult",
                table: "TestCase",
                type: "varchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "TestCase",
                keyColumn: "Environment",
                keyValue: null,
                column: "Environment",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Environment",
                table: "TestCase",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "TestCase",
                keyColumn: "Description",
                keyValue: null,
                column: "Description",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TestCase",
                type: "varchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1500)",
                oldMaxLength: 1500,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 112, DateTimeKind.Utc).AddTicks(4297),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 779, DateTimeKind.Utc).AddTicks(7079));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDate",
                table: "TestCase",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 103, DateTimeKind.Utc).AddTicks(9326),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 772, DateTimeKind.Utc).AddTicks(917));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 778, DateTimeKind.Utc).AddTicks(1027),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 111, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 773, DateTimeKind.Utc).AddTicks(7658),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 104, DateTimeKind.Utc).AddTicks(2532));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 794, DateTimeKind.Utc).AddTicks(7904),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 116, DateTimeKind.Utc).AddTicks(1183));

            migrationBuilder.AlterColumn<string>(
                name: "Steps",
                table: "TestCase",
                type: "varchar(700)",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreConditions",
                table: "TestCase",
                type: "varchar(700)",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedResult",
                table: "TestCase",
                type: "varchar(700)",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Environment",
                table: "TestCase",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TestCase",
                type: "varchar(1500)",
                maxLength: 1500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1500)",
                oldMaxLength: 1500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 779, DateTimeKind.Utc).AddTicks(7079),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 112, DateTimeKind.Utc).AddTicks(4297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CompletedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 5, 21, 40, 47, 772, DateTimeKind.Utc).AddTicks(917),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 103, DateTimeKind.Utc).AddTicks(9326));
        }
    }
}
