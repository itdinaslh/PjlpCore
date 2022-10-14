using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class InitialCreateMySQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "agama",
                columns: table => new
                {
                    AgamaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaAgama = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agama", x => x.AgamaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bidang",
                columns: table => new
                {
                    BidangID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NamaBidang = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KepalaBidang = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bidang", x => x.BidangID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.EventId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "filetypes",
                columns: table => new
                {
                    FileTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filetypes", x => x.FileTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pendidikan",
                columns: table => new
                {
                    PendidikanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaPendidikan = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pendidikan", x => x.PendidikanID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "persyaratan",
                columns: table => new
                {
                    PersyaratanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaPersyaratan = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persyaratan", x => x.PersyaratanID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "provinsi",
                columns: table => new
                {
                    ProvinsiID = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaProvinsi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HcKey = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KodeNegara = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinsi", x => x.ProvinsiID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "statuslamaran",
                columns: table => new
                {
                    StatusLamaranId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaStatus = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuslamaran", x => x.StatusLamaranId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "divisi",
                columns: table => new
                {
                    DivisiID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NamaDivisi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BidangID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_divisi", x => x.DivisiID);
                    table.ForeignKey(
                        name: "FK_divisi_bidang_BidangID",
                        column: x => x.BidangID,
                        principalTable: "bidang",
                        principalColumn: "BidangID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "jabatan",
                columns: table => new
                {
                    JabatanID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NamaJabatan = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BidangID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jabatan", x => x.JabatanID);
                    table.ForeignKey(
                        name: "FK_jabatan_bidang_BidangID",
                        column: x => x.BidangID,
                        principalTable: "bidang",
                        principalColumn: "BidangID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kabupaten",
                columns: table => new
                {
                    KabupatenID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaKabupaten = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsKota = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Latitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProvinsiID = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kabupaten", x => x.KabupatenID);
                    table.ForeignKey(
                        name: "FK_kabupaten_provinsi_ProvinsiID",
                        column: x => x.ProvinsiID,
                        principalTable: "provinsi",
                        principalColumn: "ProvinsiID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tupoksi",
                columns: table => new
                {
                    TupoksiID = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    NamaTupoksi = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DivisiID = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tupoksi", x => x.TupoksiID);
                    table.ForeignKey(
                        name: "FK_tupoksi_divisi_DivisiID",
                        column: x => x.DivisiID,
                        principalTable: "divisi",
                        principalColumn: "DivisiID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kecamatan",
                columns: table => new
                {
                    KecamatanID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaKecamatan = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KabupatenID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kecamatan", x => x.KecamatanID);
                    table.ForeignKey(
                        name: "FK_kecamatan_kabupaten_KabupatenID",
                        column: x => x.KabupatenID,
                        principalTable: "kabupaten",
                        principalColumn: "KabupatenID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "kelurahan",
                columns: table => new
                {
                    KelurahanID = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaKelurahan = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KecamatanID = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kelurahan", x => x.KelurahanID);
                    table.ForeignKey(
                        name: "FK_kelurahan_kecamatan_KecamatanID",
                        column: x => x.KecamatanID,
                        principalTable: "kecamatan",
                        principalColumn: "KecamatanID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pelamar",
                columns: table => new
                {
                    PelamarId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    NoKTP = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nama = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AgamaId = table.Column<int>(type: "int", nullable: false),
                    TglLahir = table.Column<DateOnly>(type: "date", nullable: false),
                    TempatLahir = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Kelamin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Alamat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RT = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RW = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KelurahanId = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KodePos = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DomKelurahanId = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DomAlamat = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DomRT = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DomRW = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DomKodePos = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JabatanId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PendidikanId = table.Column<int>(type: "int", nullable: false),
                    NamaSekolah = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JurusanSekolah = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoSIM = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TglAkhirSIM = table.Column<DateOnly>(type: "date", nullable: true),
                    NoKK = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GolonganDarah = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoRekening = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CabangRekening = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BidangId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Telp = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tanggungan = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoNPWP = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoBPJS = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NoBPJSK = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StatusBPJS = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsNew = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    StatusLamaranId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pelamar", x => x.PelamarId);
                    table.ForeignKey(
                        name: "FK_pelamar_agama_AgamaId",
                        column: x => x.AgamaId,
                        principalTable: "agama",
                        principalColumn: "AgamaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelamar_bidang_BidangId",
                        column: x => x.BidangId,
                        principalTable: "bidang",
                        principalColumn: "BidangID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelamar_events_EventId",
                        column: x => x.EventId,
                        principalTable: "events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelamar_jabatan_JabatanId",
                        column: x => x.JabatanId,
                        principalTable: "jabatan",
                        principalColumn: "JabatanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelamar_kelurahan_DomKelurahanId",
                        column: x => x.DomKelurahanId,
                        principalTable: "kelurahan",
                        principalColumn: "KelurahanID");
                    table.ForeignKey(
                        name: "FK_pelamar_kelurahan_KelurahanId",
                        column: x => x.KelurahanId,
                        principalTable: "kelurahan",
                        principalColumn: "KelurahanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelamar_pendidikan_PendidikanId",
                        column: x => x.PendidikanId,
                        principalTable: "pendidikan",
                        principalColumn: "PendidikanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pelamar_statuslamaran_StatusLamaranId",
                        column: x => x.StatusLamaranId,
                        principalTable: "statuslamaran",
                        principalColumn: "StatusLamaranId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_divisi_BidangID",
                table: "divisi",
                column: "BidangID");

            migrationBuilder.CreateIndex(
                name: "IX_jabatan_BidangID",
                table: "jabatan",
                column: "BidangID");

            migrationBuilder.CreateIndex(
                name: "IX_kabupaten_ProvinsiID",
                table: "kabupaten",
                column: "ProvinsiID");

            migrationBuilder.CreateIndex(
                name: "IX_kecamatan_KabupatenID",
                table: "kecamatan",
                column: "KabupatenID");

            migrationBuilder.CreateIndex(
                name: "IX_kelurahan_KecamatanID",
                table: "kelurahan",
                column: "KecamatanID");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_AgamaId",
                table: "pelamar",
                column: "AgamaId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_BidangId",
                table: "pelamar",
                column: "BidangId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_DomKelurahanId",
                table: "pelamar",
                column: "DomKelurahanId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_EventId",
                table: "pelamar",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_JabatanId",
                table: "pelamar",
                column: "JabatanId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_KelurahanId",
                table: "pelamar",
                column: "KelurahanId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_PendidikanId",
                table: "pelamar",
                column: "PendidikanId");

            migrationBuilder.CreateIndex(
                name: "IX_pelamar_StatusLamaranId",
                table: "pelamar",
                column: "StatusLamaranId");

            migrationBuilder.CreateIndex(
                name: "IX_tupoksi_DivisiID",
                table: "tupoksi",
                column: "DivisiID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "filetypes");

            migrationBuilder.DropTable(
                name: "pelamar");

            migrationBuilder.DropTable(
                name: "persyaratan");

            migrationBuilder.DropTable(
                name: "tupoksi");

            migrationBuilder.DropTable(
                name: "agama");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "jabatan");

            migrationBuilder.DropTable(
                name: "kelurahan");

            migrationBuilder.DropTable(
                name: "pendidikan");

            migrationBuilder.DropTable(
                name: "statuslamaran");

            migrationBuilder.DropTable(
                name: "divisi");

            migrationBuilder.DropTable(
                name: "kecamatan");

            migrationBuilder.DropTable(
                name: "bidang");

            migrationBuilder.DropTable(
                name: "kabupaten");

            migrationBuilder.DropTable(
                name: "provinsi");
        }
    }
}
