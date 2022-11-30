using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class UpdateFilePegawai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPjlps_jabatan_JabatanID",
                table: "DetailPjlps");

            migrationBuilder.DropForeignKey(
                name: "FK_DetailPjlps_pegawai_PegawaiID",
                table: "DetailPjlps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetailPjlps",
                table: "DetailPjlps");

            migrationBuilder.RenameTable(
                name: "DetailPjlps",
                newName: "detailpjlps");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPjlps_PegawaiID",
                table: "detailpjlps",
                newName: "IX_detailpjlps_PegawaiID");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPjlps_JabatanID",
                table: "detailpjlps",
                newName: "IX_detailpjlps_JabatanID");

            migrationBuilder.AddColumn<string>(
                name: "RealPath",
                table: "filepegawais",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_detailpjlps",
                table: "detailpjlps",
                column: "DetailPjlpID");

            migrationBuilder.AddForeignKey(
                name: "FK_detailpjlps_jabatan_JabatanID",
                table: "detailpjlps",
                column: "JabatanID",
                principalTable: "jabatan",
                principalColumn: "JabatanID");

            migrationBuilder.AddForeignKey(
                name: "FK_detailpjlps_pegawai_PegawaiID",
                table: "detailpjlps",
                column: "PegawaiID",
                principalTable: "pegawai",
                principalColumn: "PegawaiID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_detailpjlps_jabatan_JabatanID",
                table: "detailpjlps");

            migrationBuilder.DropForeignKey(
                name: "FK_detailpjlps_pegawai_PegawaiID",
                table: "detailpjlps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_detailpjlps",
                table: "detailpjlps");

            migrationBuilder.DropColumn(
                name: "RealPath",
                table: "filepegawais");

            migrationBuilder.RenameTable(
                name: "detailpjlps",
                newName: "DetailPjlps");

            migrationBuilder.RenameIndex(
                name: "IX_detailpjlps_PegawaiID",
                table: "DetailPjlps",
                newName: "IX_DetailPjlps_PegawaiID");

            migrationBuilder.RenameIndex(
                name: "IX_detailpjlps_JabatanID",
                table: "DetailPjlps",
                newName: "IX_DetailPjlps_JabatanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetailPjlps",
                table: "DetailPjlps",
                column: "DetailPjlpID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPjlps_jabatan_JabatanID",
                table: "DetailPjlps",
                column: "JabatanID",
                principalTable: "jabatan",
                principalColumn: "JabatanID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPjlps_pegawai_PegawaiID",
                table: "DetailPjlps",
                column: "PegawaiID",
                principalTable: "pegawai",
                principalColumn: "PegawaiID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
