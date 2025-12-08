using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class member_seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d958c6e5-a2fc-48ca-a5c3-7bcc77eab25b", "AQAAAAIAAYagAAAAEK/CDne0LekCnEfxWWK3Lp2yLHgRUgOTxE9o3Rb1vuYh5iy3ha7QEat6mWZrR/fHAw==", "c7bfb041-d327-48c2-bb8d-8fa9351ed645" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bad31e43-db80-4fb8-b286-9a92ea6c608a", "AQAAAAIAAYagAAAAEGFUvpeXbxm7fJeMETKTjyZiWJk3Zg7f7pcVQ8GKGU3zFanXCT7/zddHf9tbeUYU2w==", "17a1fb73-738a-46f8-94f4-dbde44e4ccde" });
        }
    }
}
