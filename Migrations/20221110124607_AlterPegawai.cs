using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterPegawai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "pegawai",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "pegawai",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "pegawai");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "pegawai");
        }
    }
}
