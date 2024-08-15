using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAmountOfTimeFromTestResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountOfTimeSpentToTest",
                table: "TestCaseResult");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 616, DateTimeKind.Utc).AddTicks(7155),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 130, DateTimeKind.Utc).AddTicks(9803));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(6587),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 129, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 620, DateTimeKind.Utc).AddTicks(5285),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 135, DateTimeKind.Utc).AddTicks(332));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 617, DateTimeKind.Utc).AddTicks(1336),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 131, DateTimeKind.Utc).AddTicks(4197));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(2255),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 129, DateTimeKind.Utc).AddTicks(2777));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 130, DateTimeKind.Utc).AddTicks(9803),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 616, DateTimeKind.Utc).AddTicks(7155));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 129, DateTimeKind.Utc).AddTicks(8053),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(6587));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AmountOfTimeSpentToTest",
                table: "TestCaseResult",
                type: "time",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 135, DateTimeKind.Utc).AddTicks(332),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 620, DateTimeKind.Utc).AddTicks(5285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 131, DateTimeKind.Utc).AddTicks(4197),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 617, DateTimeKind.Utc).AddTicks(1336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 13, 1, 21, 44, 129, DateTimeKind.Utc).AddTicks(2777),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(2255));
        }
    }
}
