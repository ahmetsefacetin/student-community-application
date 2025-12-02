using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class clubRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubRoleDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleValue = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubRoleDefinitions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClubRoleDefinitions",
                columns: new[] { "Id", "Description", "DisplayName", "RoleName", "RoleValue" },
                values: new object[,]
                {
                    { 1, "Regular club member.", "Club Member", "Member", 1 },
                    { 2, "Authorized to manage club events and members.", "Club Officer", "Officer", 2 },
                    { 3, "The main manager of the club with full permissions.", "Club Manager", "Manager", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubRoleDefinitions");
        }
    }
}
