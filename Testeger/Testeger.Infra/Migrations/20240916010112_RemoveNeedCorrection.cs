using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNeedCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NeedCorrection",
                table: "TestCase");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(5257),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 965, DateTimeKind.Utc).AddTicks(9197));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(5577),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 964, DateTimeKind.Utc).AddTicks(2913));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 588, DateTimeKind.Utc).AddTicks(7676),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 972, DateTimeKind.Utc).AddTicks(2473));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(9401),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 966, DateTimeKind.Utc).AddTicks(5804));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(896),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 963, DateTimeKind.Utc).AddTicks(5542));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 965, DateTimeKind.Utc).AddTicks(9197),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(5257));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 964, DateTimeKind.Utc).AddTicks(2913),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(5577));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 972, DateTimeKind.Utc).AddTicks(2473),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 588, DateTimeKind.Utc).AddTicks(7676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 966, DateTimeKind.Utc).AddTicks(5804),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(9401));

            migrationBuilder.AddColumn<ulong>(
                name: "NeedCorrection",
                table: "TestCase",
                type: "bit",
                nullable: false,
                defaultValue: 0ul);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 22, 22, 19, 963, DateTimeKind.Utc).AddTicks(5542),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(896));
        }
    }
}
