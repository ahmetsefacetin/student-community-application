using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ClubEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClubEvents_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33d889b4-b23c-406d-b557-1badea84fb5e", "AQAAAAIAAYagAAAAEHeF9r5LRQYUiH2jm01rGDhnd8h6KmDdiaEHe4luQLO3Hk4xSxTYLE4wY1SCqJbU3Q==", "7a4a7ce3-f5ee-4383-a798-0734c5dff825" });

            migrationBuilder.UpdateData(
                table: "ClubMemberships",
                keyColumn: "Id",
                keyValue: 99999,
                column: "JoinedAt",
                value: new DateTime(2025, 12, 26, 10, 53, 19, 894, DateTimeKind.Utc).AddTicks(3588));

            migrationBuilder.CreateIndex(
                name: "IX_ClubEvents_ClubId",
                table: "ClubEvents",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubEvents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3df8917a-c622-40c2-97de-01389562b614", "AQAAAAIAAYagAAAAEIPEToyNLyFlrEVAWMxngVb17yLZeL2OOKi1n+n4mHjjXIWuuiTTO2R/WAEHOblY5Q==", "d38aed13-8865-4faa-8296-f01e362c8aba" });

            migrationBuilder.UpdateData(
                table: "ClubMemberships",
                keyColumn: "Id",
                keyValue: 99999,
                column: "JoinedAt",
                value: new DateTime(2025, 12, 8, 20, 13, 40, 957, DateTimeKind.Utc).AddTicks(1140));
        }
    }
}
