using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class clubRolesEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "ClubMemberships",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "ClubMemberships",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.UpdateData(
                table: "ClubRoleDefinitions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Club officer with additional permissions.");

            migrationBuilder.UpdateData(
                table: "ClubRoleDefinitions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Full control of the club.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "ClubMemberships",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinedAt",
                table: "ClubMemberships",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "ClubRoleDefinitions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Authorized to manage club events and members.");

            migrationBuilder.UpdateData(
                table: "ClubRoleDefinitions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "The main manager of the club with full permissions.");
        }
    }
}
