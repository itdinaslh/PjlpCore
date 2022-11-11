using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class CreateALotOfTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pelamar_statuslamaran_StatusLamaranId",
                table: "pelamar");

            migrationBuilder.DropTable(
                name: "statuslamaran");

            migrationBuilder.CreateTable(
                name: "jenis_pegawai",
                columns: table => new
                {
                    JenisPegawaiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaJenis = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jenis_pegawai", x => x.JenisPegawaiID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "lokasi_kerja",
                columns: table => new
                {
                    LokasiKerjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaLokasi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lokasi_kerja", x => x.LokasiKerjaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.StatusId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "statuskawin",
                columns: table => new
                {
                    StatusKawinID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaStatus = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuskawin", x => x.StatusKawinID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pegawai",
                columns: table => new
                {
                    PegawaiID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NIK = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JenisPegawaiID = table.Column<int>(type: "int", nullable: false),
                    BidangID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NoKK = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NPWP = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoHP = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TempatLahir = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TglLahir = table.Column<DateOnly>(type: "date", nullable: true),
                    Kelamin = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    AgamaID = table.Column<int>(type: "int", nullable: true),
                    PendidikanID = table.Column<int>(type: "int", nullable: true),
                    JurusanPendidikan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaSekolah = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusKawinID = table.Column<int>(type: "int", nullable: true),
                    NoRekening = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CabangBank = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlamatKTP = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KelurahanID = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RtKTP = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RwKTP = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AlamatDom = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KelurahanDomID = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RtDom = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RwDom = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JabatanID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pegawai", x => x.PegawaiID);
                    table.ForeignKey(
                        name: "FK_pegawai_agama_AgamaID",
                        column: x => x.AgamaID,
                        principalTable: "agama",
                        principalColumn: "AgamaID");
                    table.ForeignKey(
                        name: "FK_pegawai_jabatan_JabatanID",
                        column: x => x.JabatanID,
                        principalTable: "jabatan",
                        principalColumn: "JabatanID");
                    table.ForeignKey(
                        name: "FK_pegawai_jenis_pegawai_JenisPegawaiID",
                        column: x => x.JenisPegawaiID,
                        principalTable: "jenis_pegawai",
                        principalColumn: "JenisPegawaiID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pegawai_kelurahan_KelurahanDomID",
                        column: x => x.KelurahanDomID,
                        principalTable: "kelurahan",
                        principalColumn: "KelurahanID");
                    table.ForeignKey(
                        name: "FK_pegawai_kelurahan_KelurahanID",
                        column: x => x.KelurahanID,
                        principalTable: "kelurahan",
                        principalColumn: "KelurahanID");
                    table.ForeignKey(
                        name: "FK_pegawai_pendidikan_PendidikanID",
                        column: x => x.PendidikanID,
                        principalTable: "pendidikan",
                        principalColumn: "PendidikanID");
                    table.ForeignKey(
                        name: "FK_pegawai_statuskawin_StatusKawinID",
                        column: x => x.StatusKawinID,
                        principalTable: "statuskawin",
                        principalColumn: "StatusKawinID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_AgamaID",
                table: "pegawai",
                column: "AgamaID");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_JabatanID",
                table: "pegawai",
                column: "JabatanID");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_JenisPegawaiID",
                table: "pegawai",
                column: "JenisPegawaiID");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_KelurahanDomID",
                table: "pegawai",
                column: "KelurahanDomID");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_KelurahanID",
                table: "pegawai",
                column: "KelurahanID");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_PendidikanID",
                table: "pegawai",
                column: "PendidikanID");

            migrationBuilder.CreateIndex(
                name: "IX_pegawai_StatusKawinID",
                table: "pegawai",
                column: "StatusKawinID");

            migrationBuilder.AddForeignKey(
                name: "FK_pelamar_status_StatusLamaranId",
                table: "pelamar",
                column: "StatusLamaranId",
                principalTable: "status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pelamar_status_StatusLamaranId",
                table: "pelamar");

            migrationBuilder.DropTable(
                name: "lokasi_kerja");

            migrationBuilder.DropTable(
                name: "pegawai");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "jenis_pegawai");

            migrationBuilder.DropTable(
                name: "statuskawin");

            migrationBuilder.CreateTable(
                name: "statuslamaran",
                columns: table => new
                {
                    StatusLamaranId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    NamaStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuslamaran", x => x.StatusLamaranId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_pelamar_statuslamaran_StatusLamaranId",
                table: "pelamar",
                column: "StatusLamaranId",
                principalTable: "statuslamaran",
                principalColumn: "StatusLamaranId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
