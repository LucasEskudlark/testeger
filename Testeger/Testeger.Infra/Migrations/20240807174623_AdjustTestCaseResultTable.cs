using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustTestCaseResultTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedTimes",
                table: "TestCaseResult");

            migrationBuilder.DropColumn(
                name: "SavedTimes1",
                table: "TestCaseResult");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(775),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 111, DateTimeKind.Utc).AddTicks(8300));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 208, DateTimeKind.Utc).AddTicks(1447),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 104, DateTimeKind.Utc).AddTicks(2532));

            migrationBuilder.AlterColumn<ulong>(
                name: "IsSuccess",
                table: "TestCaseResult",
                type: "bit",
                nullable: true,
                oldClrType: typeof(ulong),
                oldType: "bit");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "TestCaseResult",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AmountOfTimeSpentToTest",
                table: "TestCaseResult",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "ActualResult",
                table: "TestCaseResult",
                type: "varchar(700)",
                maxLength: 700,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "TestCaseResult",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 221, DateTimeKind.Utc).AddTicks(1986),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 116, DateTimeKind.Utc).AddTicks(1183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(8985),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 112, DateTimeKind.Utc).AddTicks(4297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 207, DateTimeKind.Utc).AddTicks(6053),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 103, DateTimeKind.Utc).AddTicks(9326));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "TestCaseResult");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 111, DateTimeKind.Utc).AddTicks(8300),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(775));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 104, DateTimeKind.Utc).AddTicks(2532),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 208, DateTimeKind.Utc).AddTicks(1447));

            migrationBuilder.AlterColumn<ulong>(
                name: "IsSuccess",
                table: "TestCaseResult",
                type: "bit",
                nullable: false,
                defaultValue: 0ul,
                oldClrType: typeof(ulong),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ElapsedTime",
                table: "TestCaseResult",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "AmountOfTimeSpentToTest",
                table: "TestCaseResult",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TestCaseResult",
                keyColumn: "ActualResult",
                keyValue: null,
                column: "ActualResult",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ActualResult",
                table: "TestCaseResult",
                type: "varchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(700)",
                oldMaxLength: 700,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SavedTimes",
                table: "TestCaseResult",
                type: "json",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "SavedTimes1",
                table: "TestCaseResult",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 116, DateTimeKind.Utc).AddTicks(1183),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 221, DateTimeKind.Utc).AddTicks(1986));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 112, DateTimeKind.Utc).AddTicks(4297),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 6, 22, 12, 16, 103, DateTimeKind.Utc).AddTicks(9326),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 207, DateTimeKind.Utc).AddTicks(6053));
        }
    }
}
