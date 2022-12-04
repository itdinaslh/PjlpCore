using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterUserForRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "users",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "users");

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
