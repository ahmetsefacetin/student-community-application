using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "admin-seed-001", 0, "25f608a5-bb5f-4936-b59c-0bb0e8996b4d", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@clubapp.com", true, "System", "Administrator", false, null, "ADMIN@CLUBAPP.COM", "ADMIN", "AQAAAAIAAYagAAAAEMbvjbUKTIJOhDaiEjuj5h95KLGoqVtwfvPETJw0XEqrVJI9r3lOMcwQxkFqEAyUwA==", null, false, "1e80e00b-83a2-4acf-a661-619ccdb59a88", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "admin-seed-001" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "admin-seed-001" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001");
        }
    }
}
