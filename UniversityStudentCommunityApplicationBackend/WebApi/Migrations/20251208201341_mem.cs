using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3df8917a-c622-40c2-97de-01389562b614", "AQAAAAIAAYagAAAAEIPEToyNLyFlrEVAWMxngVb17yLZeL2OOKi1n+n4mHjjXIWuuiTTO2R/WAEHOblY5Q==", "d38aed13-8865-4faa-8296-f01e362c8aba" });

            migrationBuilder.InsertData(
                table: "ClubMemberships",
                columns: new[] { "Id", "ClubId", "JoinedAt", "Role", "UserId" },
                values: new object[] { 99999, 1, new DateTime(2025, 12, 8, 20, 13, 40, 957, DateTimeKind.Utc).AddTicks(1140), 1, "5342e9a1-4ba0-424a-ad00-e4a5d482f272" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClubMemberships",
                keyColumn: "Id",
                keyValue: 99999);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d958c6e5-a2fc-48ca-a5c3-7bcc77eab25b", "AQAAAAIAAYagAAAAEK/CDne0LekCnEfxWWK3Lp2yLHgRUgOTxE9o3Rb1vuYh5iy3ha7QEat6mWZrR/fHAw==", "c7bfb041-d327-48c2-bb8d-8fa9351ed645" });
        }
    }
}
