using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class AlterPegawaiJabatan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pegawai_jabatan_JabatanID",
                table: "pegawai");

            migrationBuilder.DropIndex(
                name: "IX_pegawai_JabatanID",
                table: "pegawai");

            migrationBuilder.DropColumn(
                name: "JabatanID",
                table: "pegawai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JabatanID",
                table: "pegawai",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_JabatanID",
                table: "pegawai",
                column: "JabatanID");

            migrationBuilder.AddForeignKey(
                name: "FK_pegawai_jabatan_JabatanID",
                table: "pegawai",
                column: "JabatanID",
                principalTable: "jabatan",
                principalColumn: "JabatanID");
        }
    }
}
