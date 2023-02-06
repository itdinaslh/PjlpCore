using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class TupoksiJabatanUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tupoksi_divisi_DivisiID",
                table: "tupoksi");

            migrationBuilder.RenameColumn(
                name: "DivisiID",
                table: "tupoksi",
                newName: "JabatanID");

            migrationBuilder.RenameIndex(
                name: "IX_tupoksi_DivisiID",
                table: "tupoksi",
                newName: "IX_tupoksi_JabatanID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "filepelamar",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "filepelamar",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tupoksi_jabatan_JabatanID",
                table: "tupoksi",
                column: "JabatanID",
                principalTable: "jabatan",
                principalColumn: "JabatanID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tupoksi_jabatan_JabatanID",
                table: "tupoksi");

            migrationBuilder.RenameColumn(
                name: "JabatanID",
                table: "tupoksi",
                newName: "DivisiID");

            migrationBuilder.RenameIndex(
                name: "IX_tupoksi_JabatanID",
                table: "tupoksi",
                newName: "IX_tupoksi_DivisiID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "filepelamar",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "filepelamar",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddForeignKey(
                name: "FK_tupoksi_divisi_DivisiID",
                table: "tupoksi",
                column: "DivisiID",
                principalTable: "divisi",
                principalColumn: "DivisiID");
        }
    }
}
