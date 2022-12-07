using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterIsK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlacklisted",
                table: "pegawai");

            migrationBuilder.DropColumn(
                name: "IsK2",
                table: "pegawai");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlacklisted",
                table: "detailpjlps",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsK2",
                table: "detailpjlps",
                type: "tinyint(1)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlacklisted",
                table: "detailpjlps");

            migrationBuilder.DropColumn(
                name: "IsK2",
                table: "detailpjlps");

            migrationBuilder.AddColumn<bool>(
                name: "IsBlacklisted",
                table: "pegawai",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsK2",
                table: "pegawai",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
