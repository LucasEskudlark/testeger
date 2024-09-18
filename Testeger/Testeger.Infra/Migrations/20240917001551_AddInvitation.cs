using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testeger.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 28, DateTimeKind.Utc).AddTicks(3032),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(5257));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 26, DateTimeKind.Utc).AddTicks(7319),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(5577));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 34, DateTimeKind.Utc).AddTicks(3136),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 588, DateTimeKind.Utc).AddTicks(7676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 28, DateTimeKind.Utc).AddTicks(9309),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(9401));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 25, DateTimeKind.Utc).AddTicks(8097),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(896));

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProjectId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvitationToken = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsConfirmed = table.Column<ulong>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Invitations_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_Email",
                table: "Invitations",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_InvitationToken",
                table: "Invitations",
                column: "InvitationToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ProjectId",
                table: "Invitations",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserId",
                table: "Invitations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestRequestHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(5257),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 28, DateTimeKind.Utc).AddTicks(3032));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestRequest",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(5577),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 26, DateTimeKind.Utc).AddTicks(7319));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ChangedDate",
                table: "TestCaseHistory",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 588, DateTimeKind.Utc).AddTicks(7676),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 34, DateTimeKind.Utc).AddTicks(3136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TestCase",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 585, DateTimeKind.Utc).AddTicks(9401),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 28, DateTimeKind.Utc).AddTicks(9309));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Project",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 16, 1, 1, 11, 584, DateTimeKind.Utc).AddTicks(896),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValue: new DateTime(2024, 9, 17, 0, 15, 51, 25, DateTimeKind.Utc).AddTicks(8097));
        }
    }
}
