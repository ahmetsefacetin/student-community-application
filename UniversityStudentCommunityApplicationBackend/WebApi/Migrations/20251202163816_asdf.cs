using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class asdf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed4e355d-9d4a-47e3-90cc-5e3ae1cd326b", "AQAAAAIAAYagAAAAEJce6iriHDI9BGdLIRDwOqhak5jM8IvzkZUVFgWEvwGVgClYs2jW82P9LgiEaJLbDw==", "a4c2856e-83fe-4ebb-bd2a-0468ffac5bb4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6d325e1-85ca-4f99-b9c0-1ebd32de108e", "AQAAAAIAAYagAAAAEIonCsYJuSEsRMDXDLJWmrmVBar6Mfu4c8wgSNdXxCWvFC0vNrxprvg/Y5eUC8BNsQ==", "ed162fff-7839-4875-a76d-5bce6c8cf900" });
        }
    }
}
