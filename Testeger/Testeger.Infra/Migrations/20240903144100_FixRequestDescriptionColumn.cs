using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class FixRequestDescriptionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 235, DateTimeKind.Utc).AddTicks(1674),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 616, DateTimeKind.Utc).AddTicks(7155));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TestRequest",
                type: "varchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldMaxLength: 300)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 234, DateTimeKind.Utc).AddTicks(3302),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(6587));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 238, DateTimeKind.Utc).AddTicks(2975),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 620, DateTimeKind.Utc).AddTicks(5285));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 235, DateTimeKind.Utc).AddTicks(4991),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 617, DateTimeKind.Utc).AddTicks(1336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 233, DateTimeKind.Utc).AddTicks(9215),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(2255));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 616, DateTimeKind.Utc).AddTicks(7155),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 235, DateTimeKind.Utc).AddTicks(1674));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TestRequest",
                type: "varchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1500)",
                oldMaxLength: 1500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(6587),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 234, DateTimeKind.Utc).AddTicks(3302));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 620, DateTimeKind.Utc).AddTicks(5285),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 238, DateTimeKind.Utc).AddTicks(2975));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 617, DateTimeKind.Utc).AddTicks(1336),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 235, DateTimeKind.Utc).AddTicks(4991));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 14, 23, 45, 32, 615, DateTimeKind.Utc).AddTicks(2255),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 3, 14, 41, 0, 233, DateTimeKind.Utc).AddTicks(9215));
        }
    }
}
