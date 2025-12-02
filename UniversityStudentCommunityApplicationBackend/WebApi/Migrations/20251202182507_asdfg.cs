using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class asdfg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad31e43-db80-4fb8-b286-9a92ea6c608a", "AQAAAAIAAYagAAAAEGFUvpeXbxm7fJeMETKTjyZiWJk3Zg7f7pcVQ8GKGU3zFanXCT7/zddHf9tbeUYU2w==", "17a1fb73-738a-46f8-94f4-dbde44e4ccde" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed4e355d-9d4a-47e3-90cc-5e3ae1cd326b", "AQAAAAIAAYagAAAAEJce6iriHDI9BGdLIRDwOqhak5jM8IvzkZUVFgWEvwGVgClYs2jW82P9LgiEaJLbDw==", "a4c2856e-83fe-4ebb-bd2a-0468ffac5bb4" });
        }
    }
}
