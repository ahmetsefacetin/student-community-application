using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe659a52-8163-4a12-8533-3cab62cb507c", "AQAAAAIAAYagAAAAEEvnFaaFkfyh/vY0bdM5e9gfyImVl5uLFaOf5kBo+4FwUTzTaBfoSej1b0hIlWfosA==", "2ceddaf9-477c-4789-a3aa-509f29f76a3c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25f608a5-bb5f-4936-b59c-0bb0e8996b4d", "AQAAAAIAAYagAAAAEMbvjbUKTIJOhDaiEjuj5h95KLGoqVtwfvPETJw0XEqrVJI9r3lOMcwQxkFqEAyUwA==", "1e80e00b-83a2-4acf-a661-619ccdb59a88" });
        }
    }
}
