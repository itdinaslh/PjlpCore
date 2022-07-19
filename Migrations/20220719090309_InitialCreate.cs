using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PjlpCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "agama",
                columns: table => new
                {
                    AgamaID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamaAgama = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_agama", x => x.AgamaID);
                });

            migrationBuilder.CreateTable(
                name: "bidang",
                columns: table => new
                {
                    BidangID = table.Column<Guid>(type: "uuid", nullable: false),
                    NamaBidang = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: false),
                    KepalaBidang = table.Column<string>(type: "character varying(75)", maxLength: 75, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bidang", x => x.BidangID);
                });

            migrationBuilder.CreateTable(
                name: "provinsi",
                columns: table => new
                {
                    ProvinsiID = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    NamaProvinsi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HcKey = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Latitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Longitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    KodeNegara = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provinsi", x => x.ProvinsiID);
                });

            migrationBuilder.CreateTable(
                name: "kabupaten",
                columns: table => new
                {
                    KabupatenID = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    NamaKabupaten = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsKota = table.Column<bool>(type: "boolean", nullable: false),
                    Latitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Longitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ProvinsiID = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kabupaten", x => x.KabupatenID);
                    table.ForeignKey(
                        name: "FK_kabupaten_provinsi_ProvinsiID",
                        column: x => x.ProvinsiID,
                        principalTable: "provinsi",
                        principalColumn: "ProvinsiID");
                });

            migrationBuilder.CreateTable(
                name: "kecamatan",
                columns: table => new
                {
                    KecamatanID = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    NamaKecamatan = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Latitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Longitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    KabupatenID = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kecamatan", x => x.KecamatanID);
                    table.ForeignKey(
                        name: "FK_kecamatan_kabupaten_KabupatenID",
                        column: x => x.KabupatenID,
                        principalTable: "kabupaten",
                        principalColumn: "KabupatenID");
                });

            migrationBuilder.CreateTable(
                name: "kelurahan",
                columns: table => new
                {
                    KelurahanID = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    NamaKelurahan = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Latitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Longitude = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    KecamatanID = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kelurahan", x => x.KelurahanID);
                    table.ForeignKey(
                        name: "FK_kelurahan_kecamatan_KecamatanID",
                        column: x => x.KecamatanID,
                        principalTable: "kecamatan",
                        principalColumn: "KecamatanID");
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agama");

            migrationBuilder.DropTable(
                name: "bidang");

            migrationBuilder.DropTable(
                name: "kelurahan");

            migrationBuilder.DropTable(
                name: "kecamatan");

            migrationBuilder.DropTable(
                name: "kabupaten");

            migrationBuilder.DropTable(
                name: "provinsi");
        }
    }
}
