using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTestRequestIdFromHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_TestRequestHistory_TestRequest_TestRequestId",
               table: "TestRequestHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 912, DateTimeKind.Utc).AddTicks(1910),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(775));

            migrationBuilder.AlterColumn<string>(
                name: "TestRequestId",
                table: "TestRequestHistory",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 911, DateTimeKind.Utc).AddTicks(2957),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 208, DateTimeKind.Utc).AddTicks(1447));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 915, DateTimeKind.Utc).AddTicks(7265),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 221, DateTimeKind.Utc).AddTicks(1986));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 912, DateTimeKind.Utc).AddTicks(5469),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 910, DateTimeKind.Utc).AddTicks(9245),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 207, DateTimeKind.Utc).AddTicks(6053));

            migrationBuilder.UpdateData(
                table: "Image",
                keyColumn: "TestCaseResultId",
                keyValue: null,
                column: "TestCaseResultId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TestCaseResultId",
                table: "Image",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(775),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 912, DateTimeKind.Utc).AddTicks(1910));

            migrationBuilder.AlterColumn<string>(
                name: "TestRequestId",
                table: "TestRequestHistory",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 208, DateTimeKind.Utc).AddTicks(1447),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 911, DateTimeKind.Utc).AddTicks(2957));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 221, DateTimeKind.Utc).AddTicks(1986),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 915, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 216, DateTimeKind.Utc).AddTicks(8985),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 912, DateTimeKind.Utc).AddTicks(5469));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 17, 46, 23, 207, DateTimeKind.Utc).AddTicks(6053),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 8, 12, 12, 27, 4, 910, DateTimeKind.Utc).AddTicks(9245));

            migrationBuilder.AlterColumn<string>(
                name: "TestCaseResultId",
                table: "Image",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
