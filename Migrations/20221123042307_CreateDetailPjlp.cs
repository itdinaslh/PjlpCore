using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class CreateDetailPjlp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePegawais_pegawai_PegawaiID",
                table: "FilePegawais");

            migrationBuilder.DropForeignKey(
                name: "FK_FilePegawais_persyaratan_PersyaratanID",
                table: "FilePegawais");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilePegawais",
                table: "FilePegawais");

            migrationBuilder.RenameTable(
                name: "FilePegawais",
                newName: "filepegawais");

            migrationBuilder.RenameIndex(
                name: "IX_FilePegawais_PersyaratanID",
                table: "filepegawais",
                newName: "IX_filepegawais_PersyaratanID");

            migrationBuilder.RenameIndex(
                name: "IX_FilePegawais_PegawaiID",
                table: "filepegawais",
                newName: "IX_filepegawais_PegawaiID");

            migrationBuilder.AddColumn<string>(
                name: "NoBPJS",
                table: "pegawai",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StatusBPJS",
                table: "pegawai",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_filepegawais",
                table: "filepegawais",
                column: "FilePegawaiID");

            migrationBuilder.CreateTable(
                name: "DetailPjlps",
                columns: table => new
                {
                    DetailPjlpID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PegawaiID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tanggungan = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoBPJSK = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoSIM = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MasaBerlakuSIM = table.Column<DateOnly>(type: "date", nullable: true),
                    JabatanID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailPjlps", x => x.DetailPjlpID);
                    table.ForeignKey(
                        name: "FK_DetailPjlps_jabatan_JabatanID",
                        column: x => x.JabatanID,
                        principalTable: "jabatan",
                        principalColumn: "JabatanID");
                    table.ForeignKey(
                        name: "FK_DetailPjlps_pegawai_PegawaiID",
                        column: x => x.PegawaiID,
                        principalTable: "pegawai",
                        principalColumn: "PegawaiID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPjlps_JabatanID",
                table: "DetailPjlps",
                column: "JabatanID");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPjlps_PegawaiID",
                table: "DetailPjlps",
                column: "PegawaiID");

            migrationBuilder.AddForeignKey(
                name: "FK_filepegawais_pegawai_PegawaiID",
                table: "filepegawais",
                column: "PegawaiID",
                principalTable: "pegawai",
                principalColumn: "PegawaiID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_filepegawais_persyaratan_PersyaratanID",
                table: "filepegawais",
                column: "PersyaratanID",
                principalTable: "persyaratan",
                principalColumn: "PersyaratanID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_filepegawais_pegawai_PegawaiID",
                table: "filepegawais");

            migrationBuilder.DropForeignKey(
                name: "FK_filepegawais_persyaratan_PersyaratanID",
                table: "filepegawais");

            migrationBuilder.DropTable(
                name: "DetailPjlps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_filepegawais",
                table: "filepegawais");

            migrationBuilder.DropColumn(
                name: "NoBPJS",
                table: "pegawai");

            migrationBuilder.DropColumn(
                name: "StatusBPJS",
                table: "pegawai");

            migrationBuilder.RenameTable(
                name: "filepegawais",
                newName: "FilePegawais");

            migrationBuilder.RenameIndex(
                name: "IX_filepegawais_PersyaratanID",
                table: "FilePegawais",
                newName: "IX_FilePegawais_PersyaratanID");

            migrationBuilder.RenameIndex(
                name: "IX_filepegawais_PegawaiID",
                table: "FilePegawais",
                newName: "IX_FilePegawais_PegawaiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilePegawais",
                table: "FilePegawais",
                column: "FilePegawaiID");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePegawais_pegawai_PegawaiID",
                table: "FilePegawais",
                column: "PegawaiID",
                principalTable: "pegawai",
                principalColumn: "PegawaiID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilePegawais_persyaratan_PersyaratanID",
                table: "FilePegawais",
                column: "PersyaratanID",
                principalTable: "persyaratan",
                principalColumn: "PersyaratanID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
