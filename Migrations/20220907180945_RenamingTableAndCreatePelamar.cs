using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class RenamingTableAndCreatePelamar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_divisi_bidang_BidangID",
                table: "divisi");

            migrationBuilder.DropForeignKey(
                name: "FK_jabatan_bidang_BidangID",
                table: "jabatan");

            migrationBuilder.DropForeignKey(
                name: "FK_kabupaten_provinsi_ProvinsiID",
                table: "kabupaten");

            migrationBuilder.DropForeignKey(
                name: "FK_kecamatan_kabupaten_KabupatenID",
                table: "kecamatan");

            migrationBuilder.DropForeignKey(
                name: "FK_kelurahan_kecamatan_KecamatanID",
                table: "kelurahan");

            migrationBuilder.DropForeignKey(
                name: "FK_tupoksi_divisi_DivisiID",
                table: "tupoksi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tupoksi",
                table: "tupoksi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_provinsi",
                table: "provinsi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_persyaratan",
                table: "persyaratan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pendidikan",
                table: "pendidikan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kelurahan",
                table: "kelurahan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kecamatan",
                table: "kecamatan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kabupaten",
                table: "kabupaten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_jabatan",
                table: "jabatan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_divisi",
                table: "divisi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bidang",
                table: "bidang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_agama",
                table: "agama");

            migrationBuilder.RenameTable(
                name: "tupoksi",
                newName: "Tupoksi");

            migrationBuilder.RenameTable(
                name: "provinsi",
                newName: "Provinsi");

            migrationBuilder.RenameTable(
                name: "persyaratan",
                newName: "Persyaratan");

            migrationBuilder.RenameTable(
                name: "pendidikan",
                newName: "Pendidikan");

            migrationBuilder.RenameTable(
                name: "kelurahan",
                newName: "Kelurahan");

            migrationBuilder.RenameTable(
                name: "kecamatan",
                newName: "Kecamatan");

            migrationBuilder.RenameTable(
                name: "kabupaten",
                newName: "Kabupaten");

            migrationBuilder.RenameTable(
                name: "jabatan",
                newName: "Jabatan");

            migrationBuilder.RenameTable(
                name: "divisi",
                newName: "Divisi");

            migrationBuilder.RenameTable(
                name: "bidang",
                newName: "Bidang");

            migrationBuilder.RenameTable(
                name: "agama",
                newName: "Agama");

            migrationBuilder.RenameIndex(
                name: "IX_tupoksi_DivisiID",
                table: "Tupoksi",
                newName: "IX_Tupoksi_DivisiID");

            migrationBuilder.RenameIndex(
                name: "IX_kelurahan_KecamatanID",
                table: "Kelurahan",
                newName: "IX_Kelurahan_KecamatanID");

            migrationBuilder.RenameIndex(
                name: "IX_kecamatan_KabupatenID",
                table: "Kecamatan",
                newName: "IX_Kecamatan_KabupatenID");

            migrationBuilder.RenameIndex(
                name: "IX_kabupaten_ProvinsiID",
                table: "Kabupaten",
                newName: "IX_Kabupaten_ProvinsiID");

            migrationBuilder.RenameIndex(
                name: "IX_jabatan_BidangID",
                table: "Jabatan",
                newName: "IX_Jabatan_BidangID");

            migrationBuilder.RenameIndex(
                name: "IX_divisi_BidangID",
                table: "Divisi",
                newName: "IX_Divisi_BidangID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tupoksi",
                table: "Tupoksi",
                column: "TupoksiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinsi",
                table: "Provinsi",
                column: "ProvinsiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persyaratan",
                table: "Persyaratan",
                column: "PersyaratanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pendidikan",
                table: "Pendidikan",
                column: "PendidikanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kelurahan",
                table: "Kelurahan",
                column: "KelurahanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kecamatan",
                table: "Kecamatan",
                column: "KecamatanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kabupaten",
                table: "Kabupaten",
                column: "KabupatenID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Jabatan",
                table: "Jabatan",
                column: "JabatanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Divisi",
                table: "Divisi",
                column: "DivisiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bidang",
                table: "Bidang",
                column: "BidangID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agama",
                table: "Agama",
                column: "AgamaID");

            migrationBuilder.CreateTable(
                name: "StatusLamaran",
                columns: table => new
                {
                    StatusLamaranId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamaStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLamaran", x => x.StatusLamaranId);
                });

            migrationBuilder.CreateTable(
                name: "Pelamar",
                columns: table => new
                {
                    PelamarId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    NoKTP = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Nama = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    AgamaId = table.Column<int>(type: "integer", nullable: false),
                    TglLahir = table.Column<DateOnly>(type: "date", nullable: false),
                    TempatLahir = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Kelamin = table.Column<bool>(type: "boolean", nullable: false),
                    Alamat = table.Column<string>(type: "text", nullable: false),
                    RT = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    RW = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    KelurahanId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    KodePos = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DomKelurahanId = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    DomAlamat = table.Column<string>(type: "text", nullable: true),
                    DomRT = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    DomRW = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    DomKodePos = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    JabatanId = table.Column<Guid>(type: "uuid", nullable: false),
                    PendidikanId = table.Column<int>(type: "integer", nullable: false),
                    NamaSekolah = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    JurusanSekolah = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    NoSIM = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    TglAkhirSIM = table.Column<DateOnly>(type: "date", nullable: true),
                    NoKK = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    GolonganDarah = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    NoRekening = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CabangRekening = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    BidangId = table.Column<Guid>(type: "uuid", nullable: false),
                    Telp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Tanggungan = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    NoNPWP = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    NoBPJS = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    NoBPJSK = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    StatusBPJS = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    StatusLamaranId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pelamar", x => x.PelamarId);
                    table.ForeignKey(
                        name: "FK_Pelamar_Agama_AgamaId",
                        column: x => x.AgamaId,
                        principalTable: "Agama",
                        principalColumn: "AgamaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelamar_Bidang_BidangId",
                        column: x => x.BidangId,
                        principalTable: "Bidang",
                        principalColumn: "BidangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelamar_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelamar_Jabatan_JabatanId",
                        column: x => x.JabatanId,
                        principalTable: "Jabatan",
                        principalColumn: "JabatanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelamar_Kelurahan_DomKelurahanId",
                        column: x => x.DomKelurahanId,
                        principalTable: "Kelurahan",
                        principalColumn: "KelurahanID");
                    table.ForeignKey(
                        name: "FK_Pelamar_Kelurahan_KelurahanId",
                        column: x => x.KelurahanId,
                        principalTable: "Kelurahan",
                        principalColumn: "KelurahanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelamar_Pendidikan_PendidikanId",
                        column: x => x.PendidikanId,
                        principalTable: "Pendidikan",
                        principalColumn: "PendidikanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pelamar_StatusLamaran_StatusLamaranId",
                        column: x => x.StatusLamaranId,
                        principalTable: "StatusLamaran",
                        principalColumn: "StatusLamaranId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_AgamaId",
                table: "Pelamar",
                column: "AgamaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_BidangId",
                table: "Pelamar",
                column: "BidangId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_DomKelurahanId",
                table: "Pelamar",
                column: "DomKelurahanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_EventId",
                table: "Pelamar",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_JabatanId",
                table: "Pelamar",
                column: "JabatanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_KelurahanId",
                table: "Pelamar",
                column: "KelurahanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_PendidikanId",
                table: "Pelamar",
                column: "PendidikanId");

            migrationBuilder.CreateIndex(
                name: "IX_Pelamar_StatusLamaranId",
                table: "Pelamar",
                column: "StatusLamaranId");

            migrationBuilder.AddForeignKey(
                name: "FK_Divisi_Bidang_BidangID",
                table: "Divisi",
                column: "BidangID",
                principalTable: "Bidang",
                principalColumn: "BidangID");

            migrationBuilder.AddForeignKey(
                name: "FK_Jabatan_Bidang_BidangID",
                table: "Jabatan",
                column: "BidangID",
                principalTable: "Bidang",
                principalColumn: "BidangID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kabupaten_Provinsi_ProvinsiID",
                table: "Kabupaten",
                column: "ProvinsiID",
                principalTable: "Provinsi",
                principalColumn: "ProvinsiID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kecamatan_Kabupaten_KabupatenID",
                table: "Kecamatan",
                column: "KabupatenID",
                principalTable: "Kabupaten",
                principalColumn: "KabupatenID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kelurahan_Kecamatan_KecamatanID",
                table: "Kelurahan",
                column: "KecamatanID",
                principalTable: "Kecamatan",
                principalColumn: "KecamatanID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tupoksi_Divisi_DivisiID",
                table: "Tupoksi",
                column: "DivisiID",
                principalTable: "Divisi",
                principalColumn: "DivisiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisi_Bidang_BidangID",
                table: "Divisi");

            migrationBuilder.DropForeignKey(
                name: "FK_Jabatan_Bidang_BidangID",
                table: "Jabatan");

            migrationBuilder.DropForeignKey(
                name: "FK_Kabupaten_Provinsi_ProvinsiID",
                table: "Kabupaten");

            migrationBuilder.DropForeignKey(
                name: "FK_Kecamatan_Kabupaten_KabupatenID",
                table: "Kecamatan");

            migrationBuilder.DropForeignKey(
                name: "FK_Kelurahan_Kecamatan_KecamatanID",
                table: "Kelurahan");

            migrationBuilder.DropForeignKey(
                name: "FK_Tupoksi_Divisi_DivisiID",
                table: "Tupoksi");

            migrationBuilder.DropTable(
                name: "Pelamar");

            migrationBuilder.DropTable(
                name: "StatusLamaran");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tupoksi",
                table: "Tupoksi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinsi",
                table: "Provinsi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persyaratan",
                table: "Persyaratan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pendidikan",
                table: "Pendidikan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kelurahan",
                table: "Kelurahan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kecamatan",
                table: "Kecamatan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kabupaten",
                table: "Kabupaten");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Jabatan",
                table: "Jabatan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Divisi",
                table: "Divisi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bidang",
                table: "Bidang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agama",
                table: "Agama");

            migrationBuilder.RenameTable(
                name: "Tupoksi",
                newName: "tupoksi");

            migrationBuilder.RenameTable(
                name: "Provinsi",
                newName: "provinsi");

            migrationBuilder.RenameTable(
                name: "Persyaratan",
                newName: "persyaratan");

            migrationBuilder.RenameTable(
                name: "Pendidikan",
                newName: "pendidikan");

            migrationBuilder.RenameTable(
                name: "Kelurahan",
                newName: "kelurahan");

            migrationBuilder.RenameTable(
                name: "Kecamatan",
                newName: "kecamatan");

            migrationBuilder.RenameTable(
                name: "Kabupaten",
                newName: "kabupaten");

            migrationBuilder.RenameTable(
                name: "Jabatan",
                newName: "jabatan");

            migrationBuilder.RenameTable(
                name: "Divisi",
                newName: "divisi");

            migrationBuilder.RenameTable(
                name: "Bidang",
                newName: "bidang");

            migrationBuilder.RenameTable(
                name: "Agama",
                newName: "agama");

            migrationBuilder.RenameIndex(
                name: "IX_Tupoksi_DivisiID",
                table: "tupoksi",
                newName: "IX_tupoksi_DivisiID");

            migrationBuilder.RenameIndex(
                name: "IX_Kelurahan_KecamatanID",
                table: "kelurahan",
                newName: "IX_kelurahan_KecamatanID");

            migrationBuilder.RenameIndex(
                name: "IX_Kecamatan_KabupatenID",
                table: "kecamatan",
                newName: "IX_kecamatan_KabupatenID");

            migrationBuilder.RenameIndex(
                name: "IX_Kabupaten_ProvinsiID",
                table: "kabupaten",
                newName: "IX_kabupaten_ProvinsiID");

            migrationBuilder.RenameIndex(
                name: "IX_Jabatan_BidangID",
                table: "jabatan",
                newName: "IX_jabatan_BidangID");

            migrationBuilder.RenameIndex(
                name: "IX_Divisi_BidangID",
                table: "divisi",
                newName: "IX_divisi_BidangID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tupoksi",
                table: "tupoksi",
                column: "TupoksiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_provinsi",
                table: "provinsi",
                column: "ProvinsiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_persyaratan",
                table: "persyaratan",
                column: "PersyaratanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pendidikan",
                table: "pendidikan",
                column: "PendidikanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kelurahan",
                table: "kelurahan",
                column: "KelurahanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kecamatan",
                table: "kecamatan",
                column: "KecamatanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_kabupaten",
                table: "kabupaten",
                column: "KabupatenID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_jabatan",
                table: "jabatan",
                column: "JabatanID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_divisi",
                table: "divisi",
                column: "DivisiID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bidang",
                table: "bidang",
                column: "BidangID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_agama",
                table: "agama",
                column: "AgamaID");

            migrationBuilder.AddForeignKey(
                name: "FK_divisi_bidang_BidangID",
                table: "divisi",
                column: "BidangID",
                principalTable: "bidang",
                principalColumn: "BidangID");

            migrationBuilder.AddForeignKey(
                name: "FK_jabatan_bidang_BidangID",
                table: "jabatan",
                column: "BidangID",
                principalTable: "bidang",
                principalColumn: "BidangID");

            migrationBuilder.AddForeignKey(
                name: "FK_kabupaten_provinsi_ProvinsiID",
                table: "kabupaten",
                column: "ProvinsiID",
                principalTable: "provinsi",
                principalColumn: "ProvinsiID");

            migrationBuilder.AddForeignKey(
                name: "FK_kecamatan_kabupaten_KabupatenID",
                table: "kecamatan",
                column: "KabupatenID",
                principalTable: "kabupaten",
                principalColumn: "KabupatenID");

            migrationBuilder.AddForeignKey(
                name: "FK_kelurahan_kecamatan_KecamatanID",
                table: "kelurahan",
                column: "KecamatanID",
                principalTable: "kecamatan",
                principalColumn: "KecamatanID");

            migrationBuilder.AddForeignKey(
                name: "FK_tupoksi_divisi_DivisiID",
                table: "tupoksi",
                column: "DivisiID",
                principalTable: "divisi",
                principalColumn: "DivisiID");
        }
    }
}
