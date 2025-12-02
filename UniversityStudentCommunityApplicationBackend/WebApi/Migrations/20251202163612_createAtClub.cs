using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class createAtClub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b6d325e1-85ca-4f99-b9c0-1ebd32de108e", "AQAAAAIAAYagAAAAEIonCsYJuSEsRMDXDLJWmrmVBar6Mfu4c8wgSNdXxCWvFC0vNrxprvg/Y5eUC8BNsQ==", "ed162fff-7839-4875-a76d-5bce6c8cf900" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-seed-001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b70713f-8222-44d8-95a5-193bb3a376a2", "AQAAAAIAAYagAAAAEF7pVtaO64Mavgx06zL5XMuyxMWk2kEdjhIyCEfoSEPYLJwsp1JHI7Wj4FfdYwWlcg==", "c284f9ce-390d-484d-a3c2-a00f55067d57" });
        }
    }
}
